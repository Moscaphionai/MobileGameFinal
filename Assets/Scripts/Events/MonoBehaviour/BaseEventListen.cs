using UnityEngine;
using UnityEngine.Events;

namespace MobileGame.Events
{
    public abstract class BaseEventListen<T> : MonoBehaviour
    {
        private BaseEventSO<T> subscribedEventSo;

        protected abstract BaseEventSO<T> EventSo { get; }

        protected abstract UnityEvent<T> Response { get; }

        protected virtual void OnEnable()
        {
            Subscribe();
        }

        protected virtual void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (subscribedEventSo != null || EventSo == null)
            {
                return;
            }

            subscribedEventSo = EventSo;
            subscribedEventSo.OnEventRaised += HandleEventRaised;
        }

        private void Unsubscribe()
        {
            if (subscribedEventSo == null)
            {
                return;
            }

            subscribedEventSo.OnEventRaised -= HandleEventRaised;
            subscribedEventSo = null;
        }

        private void HandleEventRaised(T evt)
        {
            Response?.Invoke(evt);
        }
    }
}
