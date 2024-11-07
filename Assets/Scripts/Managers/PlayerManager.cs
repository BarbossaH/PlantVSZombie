using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    private int sunAmount;
    public int SunAmount
    {
        get { return sunAmount; }
        set
        {
            sunAmount = value;
            UIManager.Instance.UpdateSunAmount(sunAmount);
        }

    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SunAmount = 100;
    }
}
