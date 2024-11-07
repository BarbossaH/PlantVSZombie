using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter : SingletonMono<NotificationCenter>
{
    private List<IObserver> observers = new List<IObserver>();

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

    public void NotifyObserver(Event_Type event_Type, object data=null)
    {
        foreach (var observer in observers) { 
            observer.OnDataChanged(event_Type, data);
        }
    }
}
