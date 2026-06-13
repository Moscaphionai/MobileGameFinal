using MessageQueue;
using MessageQueue.Messages.Gameplay.Battle;
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
            
            MessageQueueManager.Instance.SendMessage(new BattleStartMessage());
        }
    }
}