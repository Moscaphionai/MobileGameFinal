using Compose.Gameplay.Battle;

namespace MessageQueue.Messages.Gameplay.Battle
{
    /// <summary>
    /// 系统间通信
    /// </summary>
    public class TurnStateChangedMessage : IMessage
    {
        public TurnState nowState;
    }
}