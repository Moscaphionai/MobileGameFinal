using System.Collections.Generic;
using Compose.Card;
using ScriptableObjects.Actors;

namespace Compose.Actors
{
    public sealed class PlayerData : ActorData
    {
        private readonly List<CardData> deck = new List<CardData>();

        public IReadOnlyList<CardData> Deck => deck;

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
        public PlayerData PlayerData => (PlayerData)Data;

        public void Init(PlayerSO playerSO)
        {
            Init(new PlayerData(playerSO));
        }
    }
}
