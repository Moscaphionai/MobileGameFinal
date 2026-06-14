using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public abstract class TurnStateBase
    {
        public abstract TurnState State { get; }

        public virtual UniTask EnterAsync()
        {
            return UniTask.CompletedTask;
        }

        public abstract UniTask ExecuteAsync();

        public virtual UniTask ExitAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}
