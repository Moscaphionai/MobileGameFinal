using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public enum CardType
    {
        Attack,
        Skill,
        Enhance
    }

    [Serializable]
    public enum CardRarity
    {
        Basis,
        Normal,
        Rare,
        Legend
    }

    [Serializable]
    public class CardInfo
    {
        public int id;
        public string name;
        public string nameEn;
        public string description;
        public string descriptionEn;
        public CardRarity rarity;
        public int cost;
        public CardType type;
    }

    [CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Cards/NewCard")]
    public class CardSO : ScriptableObject
    {
        public CardInfo info = new();
        public List<EffectInfo> effects = new();
    }
}
