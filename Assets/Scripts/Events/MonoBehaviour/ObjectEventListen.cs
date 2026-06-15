using System;
using UnityEngine;
using UnityEngine.Events;

namespace MobileGame.Events
{
    [Serializable]
    public sealed class ObjectEventResponse : UnityEvent<object>
    {
    }

    [AddComponentMenu("Mobile Game/Events/Object Event Listener")]
    public sealed class ObjectEventListen : BaseEventListen<object>
    {
        [SerializeField] private ObjectEventSO eventSo;
        [SerializeField] private ObjectEventResponse response = new ObjectEventResponse();

        protected override BaseEventSO<object> EventSo => eventSo;

        protected override UnityEvent<object> Response => response;
    }
}
