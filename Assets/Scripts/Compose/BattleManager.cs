using System.Collections.Generic;
using Compose.Actors;
using Messages;
using Messages.Commands.Battle;
using Messages.Events.Battle;
using Utilities;

namespace Compose
{
    public sealed class BattleManager : MonoSingleton<BattleManager>
    {
        private BattleStateMachine fsm;

        public PlayerData player;
        public EnemyData enemy;
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
        }

        private void OnDisable()
        {
            CommandQueueManager.Instance.RemoveListener<StartBattleCommand>(HandleCommand);
            CommandQueueManager.Instance.RemoveListener<EndPlayerTurnCommand>(HandleCommand);
            CommandQueueManager.Instance.RemoveListener<EndEnemyTurnCommand>(HandleCommand);
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

        private void StartBattle(StartBattleCommand command)
        {
            player = command.player;
            enemy = command.enemy;
            Round = 1;

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
            fsm.ChangeState(BattleTurn.Player);
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
