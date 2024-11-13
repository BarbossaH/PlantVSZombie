namespace Managers
{
    using System.Collections.Generic;
    using UnityEngine;
    using Conf;
    public class SunManager : SingletonMono<SunManager>
    {
        private readonly Dictionary<SunTypeEnum, GameObject> sunDic = new Dictionary<SunTypeEnum, GameObject>();
        protected override void Init()
        {
            SunListSO  sunSo= Resources.Load<SunListSO>("SunListSO");

            if (sunSo is null)
            {
                Debug.LogError(
                    "SunListSO asset not found in Resources. Please ensure it exists in a Resources folder.");
                return;
            }

            List<SunData> sunList = sunSo.SunList;
            foreach (var item in sunList)
            {
                sunDic.Add(item.sunType, item.sunPrefab);
            }
        }

        public GameObject GetSunPrefabByType(SunTypeEnum type)
        {
            // if (sunDic.ContainsKey(type)) return sunDic[type];
            // return null;
            sunDic.TryGetValue(type, out GameObject prefab);
            return prefab;
        }


    }
}