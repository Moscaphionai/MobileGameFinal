using System;
using System.Collections.Generic;

namespace MessageQueue
{
    public interface IMessage
    {
    }

    public class MessageQueueManager
    {
        private readonly Dictionary<Type, List<Delegate>> _listeners;

        private static MessageQueueManager _instance;

        public static MessageQueueManager Instance => _instance ??= new MessageQueueManager();

        private MessageQueueManager()
        {
            _listeners = new Dictionary<Type, List<Delegate>>();
        }

        public void AddListener<T>(Action<T> listener)
        {
            if (_listeners.TryGetValue(typeof(T), out var listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                listeners = new List<Delegate> { listener };
                _listeners.Add(typeof(T), listeners);
            }
        }

        public void RemoveListener<T>(Action<T> listener)
        {
            if (_listeners.TryGetValue(typeof(T), out var listeners))
            {
                listeners.Remove(listener);
            }
        }

        public void SendMessage(IMessage message)
        {
            if (_listeners.TryGetValue(message.GetType(), out var listeners))
            {
                for (int i = 0; i < listeners.Count; i++)
                {
                    listeners[i].DynamicInvoke(message);
                }
            }
        }
    }
}