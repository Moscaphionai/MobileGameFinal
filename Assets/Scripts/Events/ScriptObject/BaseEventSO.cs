using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MobileGame.Events
{
    public abstract class BaseEventSO : ScriptableObject
    {
        public abstract int ListenerCount { get; }

        public abstract IReadOnlyList<string> GetListenerDescriptions();
    }

    public abstract class BaseEventSO<T> : BaseEventSO
    {
        public event UnityAction<T> OnEventRaised;

        [NonSerialized] private object lastSender;

        public object LastSender => lastSender;

        public override int ListenerCount => OnEventRaised?.GetInvocationList().Length ?? 0;

        public void RaiseEvent(T evt, object sender = null)
        {
            lastSender = sender;
            OnEventRaised?.Invoke(evt);
        }

        public override IReadOnlyList<string> GetListenerDescriptions()
        {
            if (OnEventRaised == null)
            {
                return Array.Empty<string>();
            }

            Delegate[] listeners = OnEventRaised.GetInvocationList();
            var descriptions = new string[listeners.Length];

            for (int i = 0; i < listeners.Length; i++)
            {
                Delegate listener = listeners[i];
                string targetName = GetTargetName(listener.Target);
                descriptions[i] = $"{targetName}.{listener.Method.Name}";
            }

            return descriptions;
        }

        private static string GetTargetName(object target)
        {
            if (target == null)
            {
                return "static";
            }

            if (target is UnityEngine.Object unityObject)
            {
                return unityObject == null
                    ? "Missing Unity Object"
                    : $"{unityObject.name} ({target.GetType().Name})";
            }

            return target.GetType().Name;
        }
    }
}
