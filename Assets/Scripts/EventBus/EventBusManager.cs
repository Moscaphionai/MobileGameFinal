using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace EventBus
{
    public interface IEvent
    {
    }

    public sealed class EventBusManager
    {
        private sealed class ListenerEntry
        {
            public Delegate OriginalListener;
            public Func<IEvent, UniTask> InvokeAsync;
            public int Priority;
            public long RegistrationOrder;
        }

        private readonly Dictionary<Type, List<ListenerEntry>> _listeners = new();
        private long _nextRegistrationOrder;

        private static EventBusManager _instance;

        public static EventBusManager Instance => _instance ??= new EventBusManager();

        private EventBusManager()
        {
        }

        public void AddListener<T>(Action<T> listener, int priority = 100)
            where T : IEvent
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener));

            AddEntry(typeof(T), new ListenerEntry
            {
                OriginalListener = listener,
                Priority = priority,
                RegistrationOrder = _nextRegistrationOrder++,
                InvokeAsync = message =>
                {
                    listener((T)message);
                    return UniTask.CompletedTask;
                }
            });
        }

        public void AddAsyncListener<T>(Func<T, UniTask> listener, int priority = 100)
            where T : IEvent
        {
            if (listener == null)
                throw new ArgumentNullException(nameof(listener));

            AddEntry(typeof(T), new ListenerEntry
            {
                OriginalListener = listener,
                Priority = priority,
                RegistrationOrder = _nextRegistrationOrder++,
                InvokeAsync = message => listener((T)message)
            });
        }

        public void RemoveListener<T>(Action<T> listener)
            where T : IEvent
        {
            RemoveEntry(typeof(T), listener);
        }

        public void RemoveListener<T>(Func<T, UniTask> listener)
            where T : IEvent
        {
            RemoveEntry(typeof(T), listener);
        }

        public void Publish(IEvent eventData)
        {
            PublishAsync(eventData).Forget();
        }

        public async UniTask PublishAsync(IEvent eventData)
        {
            if (eventData == null)
                throw new ArgumentNullException(nameof(eventData));

            if (!_listeners.TryGetValue(eventData.GetType(), out var listeners))
                return;

            // Listeners may subscribe or unsubscribe while handling a message.
            var snapshot = listeners.ToArray();
            foreach (var entry in snapshot)
                await entry.InvokeAsync(eventData);
        }

        private void AddEntry(Type messageType, ListenerEntry entry)
        {
            if (!_listeners.TryGetValue(messageType, out var listeners))
            {
                listeners = new List<ListenerEntry>();
                _listeners.Add(messageType, listeners);
            }

            if (listeners.Exists(item => item.OriginalListener == entry.OriginalListener))
                return;

            listeners.Add(entry);
            listeners.Sort(CompareListeners);
        }

        private void RemoveEntry(Type messageType, Delegate listener)
        {
            if (!_listeners.TryGetValue(messageType, out var listeners))
                return;

            listeners.RemoveAll(item => item.OriginalListener == listener);
            if (listeners.Count == 0)
                _listeners.Remove(messageType);
        }

        private static int CompareListeners(ListenerEntry left, ListenerEntry right)
        {
            var priorityComparison = left.Priority.CompareTo(right.Priority);
            return priorityComparison != 0
                ? priorityComparison
                : left.RegistrationOrder.CompareTo(right.RegistrationOrder);
        }
    }
}
