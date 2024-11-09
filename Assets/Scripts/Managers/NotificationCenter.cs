namespace Managers
{
    using System.Collections.Generic;
  

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

        public void NotifyObserver(Event_Type eventType, object data = null)
        {
            foreach (var observer in observers)
            {
                observer.OnDataChanged(eventType, data);
            }
        }
    }
}