using Cysharp.Threading.Tasks;
using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;

namespace Compose.Gameplay.Battle.Turn
{
    public abstract class TurnStateBase
    {
        protected TurnState state;
        public TurnState State => state;

        public virtual async UniTask OnStateEnter()
        {
           await MessageQueueManager.Instance.SendMessageAsync(new TurnStateChangedMessage()
            {
                nowState = state,
            });
        }

        public virtual async UniTask Logic()
        {
            await UniTask.DelayFrame(1);
        }

        public virtual async UniTask OnStateExit()
        {
            await UniTask.DelayFrame(1);
        }
    }
}