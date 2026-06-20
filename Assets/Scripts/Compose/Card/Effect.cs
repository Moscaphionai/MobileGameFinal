using System;
using ScriptableObjects;

namespace Compose.Card
{
    public sealed class EffectData
    {
        public EffectType Type { get; }
        public int Figure { get; }
        public EffectDomain Domain { get; }

        public EffectData(EffectInfo info)
        {
            Type = info.type;
            Figure = info.figure;
            Domain = info.domain;
        }

        public int Calculate(int attack, int defense, int hp)
        {
            var domainValue = Domain switch
            {
                EffectDomain.Attack => attack,
                EffectDomain.Defense => defense,
                EffectDomain.Hp => hp,
                _ => 100
            };

            return Math.Max(0, (int)Math.Round(
                Figure * domainValue / 100f,
                MidpointRounding.AwayFromZero));
        }
    }
}
