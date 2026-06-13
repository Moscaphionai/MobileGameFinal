using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public class EnemyTurnEnd : TurnStateBase
    {
        public EnemyTurnEnd()
        {
            state = TurnState.EnemyTurnEnd;
        }
        
        public override async UniTask OnStateEnter()
        {
            await base.OnStateEnter();
        }

        public override UniTask Logic()
        {
            
        }
        
        public override UniTask OnStateExit()
        {
            
        }
    }
}