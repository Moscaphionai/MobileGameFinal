using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Utilities;

namespace Messages
{
    public interface IEvent
    {
    }

    public class EventQueueManager : Singleton<EventQueueManager>
    {
        private struct ListenerEntry
        {
            public Delegate orig;
            public Func<IEvent, UniTask> origAsync;
            public int priority;
        }

        private Dictionary<Type, List<ListenerEntry>> queues = new();

        public void AddListener<T>(Action<T> listener, int priority = 100) where T : IEvent
        {
            AddEntry(typeof(T), new ListenerEntry()
            {
                orig = listener,
                priority = priority,
                origAsync = evt =>
                {
                    listener((T)evt);
                    return UniTask.CompletedTask;
                }
            });
        }

        public void AddAsyncListener<T>(Func<T, UniTask> listener, int priority = 100) where T : IEvent
        {
            AddEntry(typeof(T), new ListenerEntry()
            {
                orig = listener,
                priority = priority,
                origAsync = evt => listener((T)evt)
            });
        }

        public void RemoveListener<T>(Action<T> listener) where T : IEvent
        {
            RemoveEntry(typeof(T), listener);
        }

        public void RemoveListener<T>(Func<T, UniTask> listener) where T : IEvent
        {
            RemoveEntry(typeof(T), listener);
        }

        public void Publish(IEvent evt)
        {
            PublishAsync(evt).Forget();
        }

        public async UniTask PublishAsync(IEvent evt)
        {
            if (!queues.TryGetValue(evt.GetType(), out var events))
            {
                return;
            }

            var snapshot = events.ToArray();
            foreach (var entry in snapshot)
            {
                await entry.origAsync(evt);
            }
        }

        private void AddEntry(Type eventType, ListenerEntry entry)
        {
            if (!queues.ContainsKey(eventType))
            {
                queues[eventType] = new List<ListenerEntry>();
            }

            queues[eventType].Add(entry);
            //priority小的先执行
            queues[eventType].Sort((x, y) => x.priority.CompareTo(y.priority));
        }
        
        private void RemoveEntry(Type eventType, Delegate entry)
        {
            if (!queues.ContainsKey(eventType))
            {
                return;
            }

            queues[eventType].RemoveAll(i => i.orig == entry);
            if (queues[eventType].Count == 0)
            {
                queues.Remove(eventType);
            }
        }
        
    }
}
