using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public sealed class EnemyTurnAct : TurnStateBase
    {
        public override TurnState State => TurnState.EnemyTurnAct;

        public override UniTask ExecuteAsync()
        {
            // Execute enemies from left to right and await each action here.
            return UniTask.CompletedTask;
        }
    }
}
