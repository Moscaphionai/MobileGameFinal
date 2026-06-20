using System;
using System.Collections.Generic;
using Compose.Effects;
using ScriptableObjects;
using UnityEngine;

namespace Compose.Card
{
    public sealed class CardData
    {
        private readonly List<EffectData> effects = new();

        public int Id { get; }
        public string Name { get; }
        public string NameEn { get; }
        public string Description { get; }
        public string DescriptionEn { get; }
        public CardRarity Rarity { get; }
        public CardType Type { get; }
        public int Cost { get; private set; }
        public IReadOnlyList<EffectData> Effects => effects;

        public CardData(CardSO cardSO)
        {
            var info = cardSO.info;
            Id = info.id;
            Name = info.name;
            NameEn = info.nameEn;
            Description = info.description;
            DescriptionEn = info.descriptionEn;
            Rarity = info.rarity;
            Type = info.type;
            Cost = info.cost;

            foreach (var effect in cardSO.effects)
            {
                effects.Add(new EffectData(effect));
            }
        }

        public void SetCost(int value)
        {
            Cost = Math.Max(0, value);
        }
    }

    public sealed class Card : MonoDul
    {
        [SerializeField] private CardPanel panel;

        public CardData Data { get; private set; }

        public void Init(CardSO cardSO)
        {
            Data = new CardData(cardSO);
            panel.Render(Data);
        }

        public void SetCost(int value)
        {
            Data.SetCost(value);
            panel.Render(Data);
        }

        public void Refresh()
        {
            panel.Render(Data);
        }
    }
}
