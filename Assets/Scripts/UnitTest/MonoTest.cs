using Compose.Gameplay.Battle;
using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;
using UnityEngine;
using Utilities;

namespace UnitTest
{
    public class MonoTest : MonoSingleton<MonoTest>
    {
        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<TurnStateChangeToMessage>(OnTurnStateChangeTo, 0);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<TurnStateChangeToMessage>(OnTurnStateChangeTo);
        }

        private void OnTurnStateChangeTo(TurnStateChangeToMessage msg)
        {
            Debug.Log($"[MonoTest] TurnState → {msg.toState}");
        }

        private void DebugSendTurnState(TurnState state)
        {
            MessageQueueManager.Instance.SendMessage(new TurnStateChangeToMessage { toState = state });
        }

        public void DebugToPlayerTurnStart() => DebugSendTurnState(TurnState.PlayerTurnStart);
        public void DebugToPlayerTurnPlay() => DebugSendTurnState(TurnState.PlayerTurnPlay);
        public void DebugToPlayerTurnEnd() => DebugSendTurnState(TurnState.PlayerTurnEnd);
        public void DebugToEnemyTurnStart() => DebugSendTurnState(TurnState.EnemyTurnStart);
        public void DebugToEnemyTurnAct() => DebugSendTurnState(TurnState.EnemyTurnAct);
        public void DebugToEnemyTurnEnd() => DebugSendTurnState(TurnState.EnemyTurnEnd);
    }
}