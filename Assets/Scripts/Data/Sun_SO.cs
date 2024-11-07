using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SunListSO", menuName = "SO/SunListSO")]
public class SunListSO : ScriptableObject
{
    public List<SunData> SunList;
}

[Serializable]
public class SunData
{
    public SunTypeEnum sunType;
    public GameObject SunPrefab;
}
