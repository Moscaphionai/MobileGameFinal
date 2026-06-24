using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Actors
{
    [Serializable]
    public sealed class EnemyInfo : ActorInfo
    {
    }

    [CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Actors/Enemy")]
    public sealed class EnemySO : ActorSO
    {
        public EnemyInfo info = new EnemyInfo();
        public List<EffectInfo> effects = new();
    }
}
