using System;
using ScriptableObjects.Actors;

namespace Compose.Actors
{
    [Serializable]
    public sealed class EnemyData : ActorData
    {
        public EnemyData(EnemyInfo info) : base(info)
        {
        }
    }

    public sealed class Enemy : Actor
    {
        public void Init(EnemySO enemySO)
        {
            Init(new EnemyData(enemySO.info));
        }
    }
}
