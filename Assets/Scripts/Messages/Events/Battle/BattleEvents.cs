using Compose.Actors;
using Compose.Effects;
using Compose;

namespace Messages.Events.Battle
{
    public enum BattleTurn
    {
        None,
        Player,
        Enemy
    }

    public sealed class BattleStartedEvent : IEvent
    {
        public PlayerData player;
        public EnemyData enemy;
    }

    public sealed class BattleTurnChangedEvent : IEvent
    {
        public BattleTurn previous;
        public BattleTurn current;
        public int round;
    }

    public sealed class ResolveEffectEvent : IEvent
    {
        public EffectData effect;
        public ActorData source;
        public PlayerData player;
        public EnemyData enemy;
        public ActorData target;
        public int value;
    }

    public sealed class BattleDataChangedEvent : IEvent
    {
        public BattleData data;
    }
}
