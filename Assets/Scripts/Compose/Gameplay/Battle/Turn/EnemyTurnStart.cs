using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public sealed class EnemyTurnStart : TurnStateBase
    {
        public override TurnState State => TurnState.EnemyTurnStart;

        public override UniTask ExecuteAsync()
        {
            // Enemy status effects are resolved here before actions begin.
            return UniTask.CompletedTask;
        }
    }
}
