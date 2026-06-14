using Compose.Gameplay.Battle;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events.Gameplay.Battle;
using UnityEngine;
using Utilities;

namespace UnitTest
{
    public class MonoTest : MonoSingleton<MonoTest>
    {
        private void OnEnable()
        {
            EventBusManager.Instance.AddListener<TurnStateChangedEvent>(OnTurnStateChanged, 0);
        }

        private void OnDisable()
        {
            EventBusManager.Instance.RemoveListener<TurnStateChangedEvent>(OnTurnStateChanged);
        }

        private void OnTurnStateChanged(TurnStateChangedEvent eventData)
        {
            Debug.Log($"[MonoTest] TurnState: {eventData.PreviousState} -> {eventData.CurrentState}");
        }

        private static void LogManagedTransition(TurnState state)
        {
            Debug.LogWarning($"[MonoTest] {state} is controlled by the turn sequence.");
        }

        public void DebugToPlayerTurnStart() => TurnManager.Instance.StartBattleAsync().Forget();
        public void DebugToPlayerTurnPlay() => LogManagedTransition(TurnState.PlayerTurnPlay);
        public void DebugToPlayerTurnEnd() => TurnManager.Instance.EndPlayerTurnAsync().Forget();
        public void DebugToEnemyTurnStart() => LogManagedTransition(TurnState.EnemyTurnStart);
        public void DebugToEnemyTurnAct() => LogManagedTransition(TurnState.EnemyTurnAct);
        public void DebugToEnemyTurnEnd() => LogManagedTransition(TurnState.EnemyTurnEnd);
    }
}
