using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public class PlayerTurnPlay : TurnStateBase
    {
        public PlayerTurnPlay()
        {
            state = TurnState.PlayerTurnPlay;
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