
using System;
using System.Collections.Generic;
using UnityEngine;
using Conf;

[CreateAssetMenu(fileName = "Bullet_SO", menuName = "SO/Bullet_SO")]
public class Bullet_SO: ScriptableObject
{
    public List<BulletData> bulletList;
}
[Serializable]
public class BulletData
{
    public PoolTypeEnum ButtetType;
    public GameObject BulletPrefab;
    public Sprite BulletHitSprite;
}
