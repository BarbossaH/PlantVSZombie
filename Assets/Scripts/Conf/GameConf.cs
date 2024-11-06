using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlantSO", menuName ="SO/PlantSo")]
public class PlantListSO : ScriptableObject
{
   public List<PlantData> plantsList;
}
[Serializable]
public class PlantData
{
    public PlantTypeEnum PlantType;
    public GameObject PlantPrefab;
}

[CreateAssetMenu(fileName ="SunSO", menuName = "SO/SunSO")]
public class SunListSO : ScriptableObject
{
    public List<SunData> SunList;
}

[Serializable]
public class SunData {
    public SunTypeEnum sunType;
    public GameObject SunPrefab;
}