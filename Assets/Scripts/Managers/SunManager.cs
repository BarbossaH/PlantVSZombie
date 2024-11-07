using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SunManager : SingletonMono<SunManager>
{

    private Dictionary<SunTypeEnum,GameObject> sunDic = new Dictionary<SunTypeEnum,GameObject>();


    protected override void Init()
    {
        SunListSO sunSO = Resources.Load<SunListSO>("SunListSO");

        if (sunSO == null)
        {
            Debug.LogError("SunListSO asset not found in Resources. Please ensure it exists in a Resources folder.");
            return;
        }
        List<SunData> sunList = sunSO.SunList;
        foreach (var item in sunList) {
            sunDic.Add(item.sunType, item.SunPrefab);
        
        } 
    }

    public GameObject GetSunPrefabByType(SunTypeEnum type)
    {
        if(sunDic.ContainsKey(type)) return sunDic[type];
        return null;
    }


}
