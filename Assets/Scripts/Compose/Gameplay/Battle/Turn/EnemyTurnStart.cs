using Cysharp.Threading.Tasks;
using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;

namespace Compose.Gameplay.Battle.Turn
{
    public class EnemyTurnStart : TurnStateBase
    {
        public EnemyTurnStart()
        {
            state = TurnState.EnemyTurnStart;
        }
        
        public override async UniTask OnStateEnter()
        {
            await base.OnStateEnter();
            await MessageQueueManager.Instance.SendMessageAsync(new TurnStateChangeToMessage()
            {
                toState = TurnState.EnemyTurnAct
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