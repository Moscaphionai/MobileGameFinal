using Cysharp.Threading.Tasks;
using Utilities;

namespace Compose.Gameplay.Battle
{
    public enum BattleState
    {
        BattleStart,
        BattleWin,
        BattleLose,
    }

    public class BattleManager : MonoSingleton<BattleManager>
    {
        public void BattleStart()
        {
            TurnManager.Instance.StartBattleAsync().Forget();
        }
    }
}
