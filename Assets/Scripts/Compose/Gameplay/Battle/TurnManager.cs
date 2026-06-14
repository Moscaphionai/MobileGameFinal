using System.Collections.Generic;
using Compose.Gameplay.Battle.Turn;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events.Gameplay.Battle;
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

    public sealed class TurnManager : MonoSingleton<TurnManager>
    {
        private readonly Dictionary<TurnState, TurnStateBase> _states = new();

        private TurnStateBase _currentState;
        private bool _isTransitioning;

        public TurnState? CurrentTurnState => _currentState?.State;
        public bool IsTransitioning => _isTransitioning;

        protected override void OnInitialize()
        {
            _states.Add(TurnState.PlayerTurnStart, new PlayerTurnStart());
            _states.Add(TurnState.PlayerTurnPlay, new PlayerTurnPlay());
            _states.Add(TurnState.PlayerTurnEnd, new PlayerTurnEnd());
            _states.Add(TurnState.EnemyTurnStart, new EnemyTurnStart());
            _states.Add(TurnState.EnemyTurnAct, new EnemyTurnAct());
            _states.Add(TurnState.EnemyTurnEnd, new EnemyTurnEnd());
        }

        public async UniTask<bool> StartBattleAsync()
        {
            if (_currentState != null || _isTransitioning)
                return false;

            _isTransitioning = true;
            try
            {
                await StartPlayerTurnAsync();
                return true;
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        public async UniTask<bool> EndPlayerTurnAsync()
        {
            if (CurrentTurnState != TurnState.PlayerTurnPlay || _isTransitioning)
                return false;

            _isTransitioning = true;
            try
            {
                await ChangeStateAsync(TurnState.PlayerTurnEnd);
                await ChangeStateAsync(TurnState.EnemyTurnStart);
                await ChangeStateAsync(TurnState.EnemyTurnAct);
                await ChangeStateAsync(TurnState.EnemyTurnEnd);
                await StartPlayerTurnAsync();
                return true;
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        private async UniTask StartPlayerTurnAsync()
        {
            await ChangeStateAsync(TurnState.PlayerTurnStart);
            await ChangeStateAsync(TurnState.PlayerTurnPlay);
        }

        private async UniTask ChangeStateAsync(TurnState nextState)
        {
            var previousState = CurrentTurnState;

            if (_currentState != null)
                await _currentState.ExitAsync();

            _currentState = _states[nextState];
            await EventBusManager.Instance.PublishAsync(new TurnStateChangedEvent
            {
                PreviousState = previousState,
                CurrentState = nextState
            });
            await _currentState.EnterAsync();
            await _currentState.ExecuteAsync();
        }
    }
}
