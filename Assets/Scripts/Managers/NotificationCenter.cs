namespace Managers
{
    using System.Collections.Generic;
    using Interfaces;
    using Conf;
    public class NotificationCenter : SingletonMono<NotificationCenter>
    {
        private readonly List<IObserver> observers = new List<IObserver>();

        public void RegisterObserver(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void UnregisterObserver(IObserver observer)
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