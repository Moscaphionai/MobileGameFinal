using System;
using UnityEngine;
using UnityEngine.Events;

namespace MobileGame.Events
{
    [Serializable]
    public sealed class IntEventResponse : UnityEvent<int>
    {
    }

    [AddComponentMenu("Mobile Game/Events/Int Event Listener")]
    public sealed class IntEventListen : BaseEventListen<int>
    {
        [SerializeField] private IntEventSO eventSo;
        [SerializeField] private IntEventResponse response = new IntEventResponse();

        protected override BaseEventSO<int> EventSo => eventSo;

        protected override UnityEvent<int> Response => response;
    }
}
