using Compose.Actors;

namespace Messages.Commands.Battle
{
    public sealed class StartBattleCommand : ICommand
    {
        public PlayerData player;
        public EnemyData enemy;
    }

    public sealed class EndPlayerTurnCommand : ICommand
    {
    }

    public sealed class EndEnemyTurnCommand : ICommand
    {
    }

    public sealed class BattleFinishedCommand : ICommand
    {
        public bool isWin;
    }

    public sealed class PlayCardCommand : ICommand
    {
        public int handIndex;
    }
}
