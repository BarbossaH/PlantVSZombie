using UnityEngine;

namespace Managers
{
    using System.Collections.Generic;
    using Interfaces;
    using Conf;
    public class NotificationCenter : SingletonMono<NotificationCenter>
    {
        private readonly List<IDataObserver> observers = new List<IDataObserver>();

        public void RegisterObserver(IDataObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void UnregisterObserver(IDataObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObserver(EventTypeEnum eventType, object data = null)
        {
            foreach (var observer in observers)
            {
                observer.OnDataChanged(eventType, data);
            }
        }
    }
}