using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameConf GameConf { get; private set; }

 
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameConf = Resources.Load<GameConf>("GameConf");
    }

}
