using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Actors
{
    [Serializable]
    public sealed class PlayerInfo : ActorInfo
    {
    }

  

    [CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObjects/Actors/Player")]
    public sealed class PlayerSO : ActorSO
    {
        public PlayerInfo info = new PlayerInfo();
        public List<CardSO> deck = new List<CardSO>();
    }
}