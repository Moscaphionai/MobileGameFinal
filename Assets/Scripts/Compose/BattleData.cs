using System;
using System.Collections.Generic;
using Compose.Actors;
using Compose.Card;

namespace Compose
{
    [Serializable]
    public sealed class BattleData
    {
        public PlayerData player;
        public EnemyData enemy;
        public int round;
        public int energy;
        public int maxEnergy = 3;
        public List<CardData> hand = new();
        public List<CardData> discardPile = new();

        public BattleData(PlayerData player, EnemyData enemy)
        {
            this.player = player;
            this.enemy = enemy;
            energy = maxEnergy;
        }
    }
}
