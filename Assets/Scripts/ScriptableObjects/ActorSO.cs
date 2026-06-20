using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public abstract class ActorInfo
    {
        public string name;
        public int atk;
        public int def;
        public int hp;
    }

    [Serializable]
    public sealed class PlayerInfo : ActorInfo
    {
    }

    [Serializable]
    public sealed class EnemyInfo : ActorInfo
    {
    }

    public abstract class ActorSO : ScriptableObject
    {
    }

    [CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObjects/Actors/Player")]
    public sealed class PlayerSO : ActorSO
    {
        public PlayerInfo info = new PlayerInfo();
        public List<CardSO> deck = new List<CardSO>();
    }

    [CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Actors/Enemy")]
    public sealed class EnemySO : ActorSO
    {
        public EnemyInfo info = new EnemyInfo();
    }
}
