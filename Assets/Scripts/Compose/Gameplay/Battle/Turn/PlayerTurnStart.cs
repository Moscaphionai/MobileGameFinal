using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public sealed class PlayerTurnStart : TurnStateBase
    {
        public override TurnState State => TurnState.PlayerTurnStart;

        public override UniTask ExecuteAsync()
        {
            // Energy recovery, block reset, status effects and card draw belong here.
            return UniTask.CompletedTask;
        }
    }
}
