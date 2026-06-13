using Cysharp.Threading.Tasks;

namespace Compose.Gameplay.Battle.Turn
{
    public class PlayerTurnEnd : TurnStateBase
    {
        public PlayerTurnEnd()
        {
            state = TurnState.PlayerTurnEnd;
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