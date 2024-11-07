using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlantListSO", menuName = "SO/PlantListSO")]
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