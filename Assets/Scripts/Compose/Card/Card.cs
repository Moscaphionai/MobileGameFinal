using System;
using System.Collections.Generic;
using Compose.Effects;
using Messages;
using Messages.Commands.Battle;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Button button;

        public CardData data;
        private int handIndex;

        private void OnEnable()
        {
            button.onClick.AddListener(Play);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Play);
        }

        public void Init(CardSO cardSO)
        {
            data = new CardData(cardSO);
            handIndex = -1;
            Refresh();
        }

        public void Init(CardData data, int handIndex)
        {
            this.data = data;
            this.handIndex = handIndex;
            Refresh();
        }

        public void Refresh()
        {
            view.Render(data);
        }

        private void Play()
        {
            CommandQueueManager.Instance.Send(new PlayCardCommand
            {
                handIndex = handIndex
            });
        }
    }
}
