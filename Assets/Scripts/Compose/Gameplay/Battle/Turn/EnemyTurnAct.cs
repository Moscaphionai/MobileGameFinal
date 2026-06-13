using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public class EnemyTurnAct : TurnStateBase
    {
        public EnemyTurnAct()
        {
            state = TurnState.EnemyTurnAct;
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