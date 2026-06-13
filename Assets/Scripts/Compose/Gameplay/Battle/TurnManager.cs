using System;
using Compose.Gameplay.Battle.Turn;
using Cysharp.Threading.Tasks;
using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;
using UnityEngine.Assertions;
using Utilities;

namespace Compose.Gameplay.Battle
{
    public enum TurnState
    {
        PlayerTurnStart,
        PlayerTurnPlay,
        PlayerTurnEnd,
        EnemyTurnStart,
        EnemyTurnAct,
        EnemyTurnEnd
    }

    public class TurnManager : MonoSingleton<TurnManager>
    {
        private TurnStateBase currentState;
        public TurnState CurrentTurnState => currentState.State;

        private TurnStateBase playerTurnStart;
        private TurnStateBase playerTurnPlay;
        private TurnStateBase playerTurnEnd;
        private TurnStateBase enemyTurnStart;
        private TurnStateBase enemyTurnAct;
        private TurnStateBase enemyTurnEnd;

        public void Awake()
        {
            playerTurnStart = new PlayerTurnStart();
            playerTurnPlay = new PlayerTurnPlay();
            playerTurnEnd = new PlayerTurnEnd();
            enemyTurnStart = new EnemyTurnStart();
            enemyTurnAct = new EnemyTurnAct();
            enemyTurnEnd = new EnemyTurnEnd();
        }

        private void Update()
        {
            currentState.Logic();
        }
        
        private async UniTask TurnStateChangeTo(TurnState toState)
        { 
            TurnStateBase next = toState switch
            {
                TurnState.PlayerTurnStart => playerTurnStart,
                TurnState.PlayerTurnPlay => playerTurnPlay,
                TurnState.PlayerTurnEnd => playerTurnEnd,
                TurnState.EnemyTurnStart => enemyTurnStart,
                TurnState.EnemyTurnAct => enemyTurnAct,
                TurnState.EnemyTurnEnd => enemyTurnEnd,
            };
            await currentState?.OnStateExit();
            currentState = next;
            await currentState.OnStateEnter();
            await currentState.Logic();
        }

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<BattleStartMessage>(OnBattleStart, 4000);
            MessageQueueManager.Instance.AddListener<TurnStateChangeToMessage>(OnTurnStateChangeTo, 0);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<BattleStartMessage>(OnBattleStart);
            MessageQueueManager.Instance.RemoveListener<TurnStateChangeToMessage>(OnTurnStateChangeTo);
        }


        private void OnTurnStateChangeTo(TurnStateChangeToMessage msg)
        {
            TurnStateChangeTo(msg.toState);
        }

        private void OnBattleStart(BattleStartMessage msg)
        {
            TurnStateChangeTo(TurnState.PlayerTurnStart);
        }
    }
}