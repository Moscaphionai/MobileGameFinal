using System;
using System.Collections.Generic;
using Compose.Card;
using ScriptableObjects.Actors;

namespace Compose.Actors
{
    [Serializable]
    public sealed class PlayerData : ActorData
    {
        public List<CardData> deck = new();
        public PlayerData(PlayerSO playerSO) : base(playerSO.info)
        {
            foreach (var cardSO in playerSO.deck)
            {
                deck.Add(new CardData(cardSO));
            }
        }
    }

    public sealed class Player : Actor
    {
        public void Init(PlayerSO playerSO)
        {
            Init(new PlayerData(playerSO));
        }
    }
}
