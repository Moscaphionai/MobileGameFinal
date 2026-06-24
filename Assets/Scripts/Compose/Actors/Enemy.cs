using System;
using System.Collections.Generic;
using Compose.Effects;
using ScriptableObjects.Actors;

namespace Compose.Actors
{
    [Serializable]
    public sealed class EnemyData : ActorData
    {
        public List<EffectData> effects = new();

        public EnemyData(EnemySO enemySO) : base(enemySO.info, enemySO.idle)
        {
            foreach (var effect in enemySO.effects)
            {
                effects.Add(new EffectData(effect));
            }
        }
    }

    public sealed class Enemy : Actor
    {
        public void Init(EnemySO enemySO)
        {
            Init(new EnemyData(enemySO));
        }
    }
}
