using System;

namespace ScriptableObjects
{
    public enum EffectType
    {
        Damage = 0,
        Shield = 1,
        Heal = 2,
        Strength = 3,
        Resistance = 4
    }

    public enum EffectDomain
    {
        Fixed,
        Attack,
        Defense,
        Hp
    }

    [Serializable]
    public sealed class EffectInfo
    {
        public EffectType type;
        public int figure;
        public EffectDomain domain;

        public static EffectInfo Parse(EffectType type, string expression)
        {
            var value = expression.Trim();

            if (int.TryParse(value, out var fixedValue))
            {
                return new EffectInfo
                {
                    type = type,
                    figure = fixedValue,
                    domain = EffectDomain.Fixed
                };
            }

            value = value.Trim('{', '}');
            var parts = value.Split(':');

            return new EffectInfo
            {
                type = type,
                figure = int.Parse(parts[0]),
                domain = ParseDomain(parts[1])
            };
        }

        private static EffectDomain ParseDomain(string value)
        {
            switch (value.Trim().ToLowerInvariant())
            {
                case "atk":
                    return EffectDomain.Attack;
                case "def":
                    return EffectDomain.Defense;
                case "hp":
                    return EffectDomain.Hp;
                default:
                    throw new FormatException($"Unknown effect domain: {value}");
            }
        }
    }
}
