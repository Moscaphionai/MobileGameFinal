using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageQueue
{
    public interface IMessage
    {
    }

    public class MessageQueueManager
    {
        private struct ListenerEntry
        {
            public Delegate Listener;
            public int Priority;
        }

        private readonly Dictionary<Type, List<ListenerEntry>> _listeners;

        private static MessageQueueManager _instance;

        public static MessageQueueManager Instance => _instance ??= new MessageQueueManager();

        private MessageQueueManager()
        {
            _listeners = new Dictionary<Type, List<ListenerEntry>>();
        }

        public void AddListener<T>(Action<T> listener)
        {
            AddListener(listener, 0);
        }

        public void AddListener<T>(Action<T> listener, int priority)
        {
            var entry = new ListenerEntry { Listener = listener, Priority = priority };
            if (_listeners.TryGetValue(typeof(T), out var listeners))
            {
                int index = listeners.FindIndex(e => e.Priority > priority);
                if (index >= 0)
                    listeners.Insert(index, entry);
                else
                    listeners.Add(entry);
            }
            else
            {
                listeners = new List<ListenerEntry> { entry };
                _listeners.Add(typeof(T), listeners);
            }
        }

        public void RemoveListener<T>(Action<T> listener)
        {
            if (_listeners.TryGetValue(typeof(T), out var listeners))
            {
                int index = listeners.FindIndex(e => e.Listener == listener);
                if (index >= 0)
                    listeners.RemoveAt(index);
            }
        }

        public void SendMessage(IMessage message)
        {
            if (_listeners.TryGetValue(message.GetType(), out var listeners))
            {
                listeners.ForEach(e => e.Listener.DynamicInvoke(message));
            }
        }
    }
}