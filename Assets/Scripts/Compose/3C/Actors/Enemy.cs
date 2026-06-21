using ScriptableObjects.Actors;

namespace Compose._3C.Actors
{
    public sealed class EnemyData : ActorData
    {
        public EnemyData(EnemyInfo info) : base(info)
        {
        }
    }

    public sealed class Enemy : Actor
    {
        public EnemyData EnemyData => (EnemyData)Data;

        public void Init(EnemySO enemySO)
        {
            Init(new EnemyData(enemySO.info));
        }
    }
}
