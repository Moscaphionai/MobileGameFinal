using Compose.Gameplay.Battle;

namespace EventBus.Events.Gameplay.Battle
{
    public sealed class TurnStateChangedEvent : IEvent
    {
        public TurnState? PreviousState;
        public TurnState CurrentState;
    }
}
