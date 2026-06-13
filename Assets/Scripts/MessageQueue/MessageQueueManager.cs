using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

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
            public bool IsAsync;
        }

        private readonly Dictionary<Type, List<ListenerEntry>> _listeners;

        private static MessageQueueManager _instance;

        public static MessageQueueManager Instance => _instance ??= new MessageQueueManager();

        private MessageQueueManager()
        {
            _listeners = new Dictionary<Type, List<ListenerEntry>>();
        }

        // ---- 私有辅助 ----

        private void InsertListener(Type messageType, ListenerEntry entry)
        {
            if (_listeners.TryGetValue(messageType, out var listeners))
            {
                int index = listeners.FindIndex(e => e.Priority > entry.Priority);
                if (index >= 0)
                    listeners.Insert(index, entry);
                else
                    listeners.Add(entry);
            }
            else
            {
                listeners = new List<ListenerEntry> { entry };
                _listeners.Add(messageType, listeners);
            }
        }

        // ---- 同步监听器注册/注销（向后兼容） ----

        public void AddListener<T>(Action<T> listener)
        {
            AddListener(listener, 100);
        }

        public void AddListener<T>(Action<T> listener, int priority)
        {
            var entry = new ListenerEntry { Listener = listener, Priority = priority, IsAsync = false };
            InsertListener(typeof(T), entry);
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

        // ---- 异步监听器注册/注销（新增） ----

        public void AddAsyncListener<T>(Func<T, UniTask> listener)
        {
            AddAsyncListener(listener, 0);
        }

        public void AddAsyncListener<T>(Func<T, UniTask> listener, int priority)
        {
            var entry = new ListenerEntry { Listener = listener, Priority = priority, IsAsync = true };
            InsertListener(typeof(T), entry);
        }

        public void RemoveListener<T>(Func<T, UniTask> listener)
        {
            if (_listeners.TryGetValue(typeof(T), out var listeners))
            {
                int index = listeners.FindIndex(e => e.Listener == listener);
                if (index >= 0)
                    listeners.RemoveAt(index);
            }
        }

        // ---- 发送消息 ----

        public void SendMessage(IMessage message)
        {
            if (_listeners.TryGetValue(message.GetType(), out var listeners))
            {
                foreach (var entry in listeners)
                {
                    if (entry.IsAsync)
                    {
                        ((Func<IMessage, UniTask>)entry.Listener).Invoke(message).Forget();
                    }
                    else
                    {
                        entry.Listener.DynamicInvoke(message);
                    }
                }
            }
        }

        public async UniTask SendMessageAsync(IMessage message)
        {
            if (_listeners.TryGetValue(message.GetType(), out var listeners))
            {
                foreach (var entry in listeners)
                {
                    if (entry.IsAsync)
                    {
                        await ((Func<IMessage, UniTask>)entry.Listener).Invoke(message);
                    }
                    else
                    {
                        entry.Listener.DynamicInvoke(message);
                    }
                }
            }
        }
    }
}
