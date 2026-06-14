using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public sealed class EnemyTurnEnd : TurnStateBase
    {
        public override TurnState State => TurnState.EnemyTurnEnd;

        public override UniTask ExecuteAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}
