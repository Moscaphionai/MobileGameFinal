using Compose.Actors;
using ScriptableObjects;

namespace Messages.Events.Battle
{
    public class DealDamageEvent : IEvent
    {
        public EffectInfo info;
        public ActorData source;
        public ActorData target;
    }
}