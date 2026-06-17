using System;
using UnityEngine;

namespace Utilities
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            Instance = this as T;
        }
    }
}