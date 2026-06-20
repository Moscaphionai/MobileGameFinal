using ScriptableObjects;

namespace Compose.Effects
{
    public struct EffectData
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
    }
}