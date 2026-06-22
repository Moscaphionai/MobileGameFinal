using System;
using System.Collections.Generic;
using Compose.Effects;
using ScriptableObjects;
using UnityEngine;

namespace Compose.Card
{
    [Serializable]
    public sealed class CardData
    {
        public int id;
        public string name;
        public string nameEn;
        public string description;
        public CardRarity rarity;
        public CardType type;
        public int cost;
        public List<EffectData> effects = new();

        public CardData(CardSO cardSO)
        {
            var info = cardSO.info;
            id = info.id;
            name = info.name;
            nameEn = info.nameEn;
            description = info.description;
            rarity = info.rarity;
            type = info.type;
            cost = info.cost;

            foreach (var effect in cardSO.effects)
            {
                effects.Add(new EffectData(effect));
            }
        }
    }

    public sealed class Card : MonoDul
    {
        [SerializeField] private CardView view;

        public CardData data;

        public void Init(CardSO cardSO)
        {
            data = new CardData(cardSO);
            Refresh();
        }

        public void Refresh()
        {
            view.Render(data);
        }
    }
}
