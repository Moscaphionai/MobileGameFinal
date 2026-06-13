using Compose.Gameplay.Battle;

namespace MessageQueue.Messages.Gameplay.Battle
{
    /// <summary>
    /// UI以及单元测试使用
    /// </summary>
    public class TurnStateChangeToMessage : IMessage
    {
        public TurnState toState;
    }
}