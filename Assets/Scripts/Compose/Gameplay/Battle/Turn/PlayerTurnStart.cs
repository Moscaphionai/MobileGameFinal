using Cysharp.Threading.Tasks;
using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;

namespace Compose.Gameplay.Battle.Turn
{
    public class PlayerTurnStart : TurnStateBase
    {
        public PlayerTurnStart()
        {
            state = TurnState.PlayerTurnStart;
        }
        public override async UniTask OnStateEnter()
        {
            await base.OnStateEnter();
            await MessageQueueManager.Instance.SendMessageAsync(new TurnStateChangeToMessage
            {
                toState = TurnState.PlayerTurnPlay
            });
        }
        public override UniTask Logic()
        {
            
        }
        public override UniTask OnStateExit()
        {
            
        }
    }
}