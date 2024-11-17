using System;
using System.Collections.Generic;
using Conf;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie_SO",menuName = "SO/Zombie_SO")]
public class Zombie_SO : ScriptableObject
{
    public GameObject zombieHead;
    public List<ZombieData> zombieDataList;
}

[Serializable]
public class ZombieData
{
    public  ZombieTypeEnum zombieType;
    public  GameObject zombiePrefab;
}