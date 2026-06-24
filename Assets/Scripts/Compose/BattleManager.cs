using System.Collections.Generic;
using Compose.Actors;
using Compose.Card;
using Compose.Effects;
using Messages;
using Messages.Commands.Battle;
using Messages.Events.Battle;
using ScriptableObjects;
using Utilities;

namespace Compose
{
    public sealed class BattleManager : MonoSingleton<BattleManager>
    {
        private BattleStateMachine fsm;

        public PlayerData player;
        public EnemyData enemy;
        public BattleData data;
        public BattleTurn Turn => fsm.Current.Turn;
        public int Round { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            fsm = new BattleStateMachine(this);
            fsm.AddState(new NoneState(this));
            fsm.AddState(new PlayerTurnState(this));
            fsm.AddState(new EnemyTurnState(this));
            fsm.SetInitialState(BattleTurn.None);
        }

        private void OnEnable()
        {
            CommandQueueManager.Instance.AddListener<StartBattleCommand>(HandleCommand);
            CommandQueueManager.Instance.AddListener<EndPlayerTurnCommand>(HandleCommand);
            CommandQueueManager.Instance.AddListener<EndEnemyTurnCommand>(HandleCommand);
            CommandQueueManager.Instance.AddListener<PlayCardCommand>(HandleCommand);

            EventQueueManager.Instance.AddListener<ResolveEffectEvent>(ResolveEffectValue, 0);
            EventQueueManager.Instance.AddListener<ResolveEffectEvent>(ResolveEffectTarget, 10);
            EventQueueManager.Instance.AddListener<ResolveEffectEvent>(ResolveEffectApply, 20);
            EventQueueManager.Instance.AddListener<ResolveEffectEvent>(NotifyBattleDataChanged, 90);
        }

        private void OnDisable()
        {
            CommandQueueManager.Instance.RemoveListener<StartBattleCommand>(HandleCommand);
            CommandQueueManager.Instance.RemoveListener<EndPlayerTurnCommand>(HandleCommand);
            CommandQueueManager.Instance.RemoveListener<EndEnemyTurnCommand>(HandleCommand);
            CommandQueueManager.Instance.RemoveListener<PlayCardCommand>(HandleCommand);

            EventQueueManager.Instance.RemoveListener<ResolveEffectEvent>(ResolveEffectValue);
            EventQueueManager.Instance.RemoveListener<ResolveEffectEvent>(ResolveEffectTarget);
            EventQueueManager.Instance.RemoveListener<ResolveEffectEvent>(ResolveEffectApply);
            EventQueueManager.Instance.RemoveListener<ResolveEffectEvent>(NotifyBattleDataChanged);
        }

        private void HandleCommand(StartBattleCommand command)
        {
            fsm.Handle(command);
        }

        private void HandleCommand(EndPlayerTurnCommand command)
        {
            fsm.Handle(command);
        }

        private void HandleCommand(EndEnemyTurnCommand command)
        {
            fsm.Handle(command);
        }

        private void HandleCommand(PlayCardCommand command)
        {
            fsm.Handle(command);
        }

        private void StartBattle(StartBattleCommand command)
        {
            player = command.player;
            enemy = command.enemy;
            Round = 1;
            data = new BattleData(player, enemy)
            {
                round = Round
            };
            data.hand.AddRange(player.deck);

            EventQueueManager.Instance.Publish(new BattleStartedEvent
            {
                player = player,
                enemy = enemy
            });

            fsm.ChangeState(BattleTurn.Player);
        }

        private void StartNextRound()
        {
            Round++;
            data.round = Round;
            data.energy = data.maxEnergy;

            fsm.ChangeState(BattleTurn.Player);
        }

        private void PlayCard(PlayCardCommand command)
        {
            if (Turn != BattleTurn.Player)
                return;

            if (command.handIndex < 0 || command.handIndex >= data.hand.Count)
                return;

            var card = data.hand[command.handIndex];
            if (data.energy < card.cost)
                return;

            data.energy -= card.cost;
            data.hand.RemoveAt(command.handIndex);

            foreach (var effect in card.effects)
            {
                EventQueueManager.Instance.Publish(new ResolveEffectEvent
                {
                    effect = effect,
                    source = player,
                    player = player,
                    enemy = enemy
                });
            }

            data.discardPile.Add(card);
            EventQueueManager.Instance.Publish(new BattleDataChangedEvent
            {
                data = data
            });
        }

        private void ResolveEffectValue(ResolveEffectEvent evt)
        {
            var sourceValue = evt.effect.domain switch
            {
                EffectDomain.Atk => evt.source.atk,
                EffectDomain.Def => evt.source.def,
                EffectDomain.Hp => evt.source.hp,
                _ => 0
            };

            evt.value = sourceValue * evt.effect.magnification / 100;
        }

        private void ResolveEffectTarget(ResolveEffectEvent evt)
        {
            evt.target = evt.effect.target switch
            {
                EffectTarget.Player => evt.player,
                EffectTarget.SingleEnemy => evt.enemy,
                EffectTarget.AllEnemy => evt.enemy,
                _ => evt.enemy
            };
        }

        private void ResolveEffectApply(ResolveEffectEvent evt)
        {
            switch (evt.effect.type)
            {
                case EffectType.Damage:
                    ApplyDamage(evt.target, evt.value);
                    break;
                case EffectType.Shield:
                    evt.target.shield += evt.value;
                    break;
                case EffectType.Heal:
                    evt.target.curHp += evt.value;
                    if (evt.target.curHp > evt.target.hp)
                        evt.target.curHp = evt.target.hp;
                    break;
            }
        }

        private void ApplyDamage(ActorData target, int value)
        {
            var shieldDamage = value;
            if (shieldDamage > target.shield)
                shieldDamage = target.shield;

            target.shield -= shieldDamage;
            value -= shieldDamage;
            target.curHp -= value;

            if (target.curHp < 0)
                target.curHp = 0;
        }

        private void NotifyBattleDataChanged(ResolveEffectEvent evt)
        {
            EventQueueManager.Instance.Publish(new BattleDataChangedEvent
            {
                data = data
            });
        }

        private abstract class BattleState
        {
            protected readonly BattleManager battle;

            public abstract BattleTurn Turn { get; }

            protected BattleState(BattleManager battle)
            {
                this.battle = battle;
            }

            public virtual void Enter()
            {
            }

            public virtual void Exit()
            {
            }

            public abstract void Handle(ICommand command);
        }

        private sealed class NoneState : BattleState
        {
            public override BattleTurn Turn => BattleTurn.None;

            public NoneState(BattleManager battle) : base(battle)
            {
            }

            public override void Handle(ICommand command)
            {
                if (command is StartBattleCommand startBattle)
                    battle.StartBattle(startBattle);
            }
        }

        private sealed class PlayerTurnState : BattleState
        {
            public override BattleTurn Turn => BattleTurn.Player;

            public PlayerTurnState(BattleManager battle) : base(battle)
            {
            }

            public override void Handle(ICommand command)
            {
                if (command is PlayCardCommand playCard)
                    battle.PlayCard(playCard);

                if (command is EndPlayerTurnCommand)
                    battle.fsm.ChangeState(BattleTurn.Enemy);
            }
        }

        private sealed class EnemyTurnState : BattleState
        {
            public override BattleTurn Turn => BattleTurn.Enemy;

            public EnemyTurnState(BattleManager battle) : base(battle)
            {
            }

            public override void Handle(ICommand command)
            {
                if (command is EndEnemyTurnCommand)
                    battle.StartNextRound();
            }
        }

        private sealed class BattleStateMachine
        {
            private readonly BattleManager battle;
            private readonly Dictionary<BattleTurn, BattleState> states = new();

            public BattleState Current { get; private set; }

            public BattleStateMachine(BattleManager battle)
            {
                this.battle = battle;
            }

            public void AddState(BattleState state)
            {
                states[state.Turn] = state;
            }

            public void SetInitialState(BattleTurn turn)
            {
                Current = states[turn];
                Current.Enter();
            }

            public void ChangeState(BattleTurn turn)
            {
                var previous = Current.Turn;
                Current.Exit();
                Current = states[turn];

                EventQueueManager.Instance.Publish(new BattleTurnChangedEvent
                {
                    previous = previous,
                    current = Current.Turn,
                    round = battle.Round
                });

                Current.Enter();
            }

            public void Handle(ICommand command)
            {
                Current.Handle(command);
            }
        }
    }
}
