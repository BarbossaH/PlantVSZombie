
using AIFSM;
using Managers;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ZombieBase zombie;
    
    public void OnMouseDown()
    {
        GridManager.Instance.ClearAllPlant();
    }
}
