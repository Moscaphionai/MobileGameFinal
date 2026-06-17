using System;
using System.Collections.Generic;
using Utilities;

namespace Messages
{
    public interface ICommand
    {
    }

    public class CommandQueueManager : Singleton<CommandQueueManager>
    {
        private readonly Dictionary<Type, List<Delegate>> queues = new();

        public void AddListener<T>(Action<T> listener) where T : ICommand
        {
            var type = typeof(T);

            if (!queues.TryGetValue(type, out var listeners))
            {
                listeners = new List<Delegate>();
                queues[type] = listeners;
            }

            listeners.Add(listener);
        }

        public void RemoveListener<T>(Action<T> listener) where T : ICommand
        {
            var type = typeof(T);

            if (!queues.TryGetValue(type, out var listeners))
                return;

            listeners.Remove(listener);

            if (listeners.Count == 0)
                queues.Remove(type);
        }

        public void Send<T>(T command) where T : ICommand
        {
            var type = typeof(T);

            if (!queues.TryGetValue(type, out var listeners))
                return;

            foreach (var listener in listeners.ToArray())
            {
                ((Action<T>)listener).Invoke(command);
            }
        }
    }
}
