using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void OnDataChanged(Event_Type type, object eventData=null); 
}
