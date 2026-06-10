using Utilities.Enum.Gameplay.Battle;

namespace MessageQueue.Messages.Gameplay.Battle
{
    public class TurnStateChangedMessage : IMessage
    {
        public TurnState oldState;
        public TurnState newState;
    }
}