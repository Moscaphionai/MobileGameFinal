namespace Utilities.Enum.Gameplay.Battle
{
    public enum BattleState
    {
        BattleStart,
        BattleWin,
        BattleLose,
    }

    public enum TurnState
    {
        PlayerTurnStart,
        PlayerPlay,
        PlayerTurnEnd,
        EnemyTurnStart,
        EnemyAction,
        EnemyTurnEnd
    }
}