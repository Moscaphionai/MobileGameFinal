using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Actors
{
    [Serializable]
    public abstract class ActorInfo
    {
        public string name;
        public int atk;
        public int def;
        public int hp;
    }

    public abstract class ActorSO : ScriptableObject
    {
    }


}
