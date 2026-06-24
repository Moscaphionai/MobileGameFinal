using ScriptableObjects;

namespace Compose.Effects
{
    public struct EffectData
    {
        public EffectType type;
        public int magnification;
        public EffectDomain domain;
        public EffectTarget target;

        public EffectData(EffectInfo info)
        {
            type = info.type;
            magnification = info.magnification;
            domain = info.domain;
            target = info.target;
        }
    }
}
