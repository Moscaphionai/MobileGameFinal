using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public sealed class PlayerTurnPlay : TurnStateBase
    {
        public override TurnState State => TurnState.PlayerTurnPlay;

        public override UniTask ExecuteAsync()
        {
            // The sequence pauses here until EndPlayerTurnAsync is called.
            return UniTask.CompletedTask;
        }
    }
}
