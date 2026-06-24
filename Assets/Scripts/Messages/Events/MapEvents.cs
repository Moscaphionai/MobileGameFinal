using Compose;

namespace Messages.Events.Map
{
    public sealed class MapDataChangedEvent : IEvent
    {
        public MapNodeData curNode;
    }
}
