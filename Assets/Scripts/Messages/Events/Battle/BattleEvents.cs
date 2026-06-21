using Compose.Actors;

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
}
