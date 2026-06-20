using System;

namespace ScriptableObjects
{
    public enum EffectType
    {
        Damage,
        Shield,
        Heal 
    }

    public enum EffectDomain
    {
        Atk,
        Def,
        Hp
    }

    public enum EffectTarget
    {
        Player,
        SingleEnemy,
        AllEnemy
    }
    
    [Serializable]
    public struct EffectInfo
    {
        public EffectType type;
        public int figure;
        public EffectDomain domain;
        public EffectTarget target;
    }
}
