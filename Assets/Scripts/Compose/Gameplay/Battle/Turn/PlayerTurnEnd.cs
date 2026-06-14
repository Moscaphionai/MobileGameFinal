using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public sealed class PlayerTurnEnd : TurnStateBase
    {
        public override TurnState State => TurnState.PlayerTurnEnd;

        public override UniTask ExecuteAsync()
        {
            // Discard the remaining hand and resolve end-of-turn effects here.
            return UniTask.CompletedTask;
        }
    }
}
