using Conf;

namespace Managers
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PlantManager : SingletonMono<PlantManager>
    {
        //this class is for managing the data of all plants, via this class, I can get any plants I want due to the plant type
        private readonly Dictionary<PlantTypeEnum, GameObject> plantDic = new Dictionary<PlantTypeEnum, GameObject>();

        protected override void Init()
        {
            PlantListSO plantListSO = Resources.Load<PlantListSO>("PlantListSO");
            if (plantListSO == null)
            {
                Debug.LogError("PlantSO asset not found in Resources. Please ensure it exists in a Resources folder.");
                return;
            }

            List<PlantData> plantList = plantListSO.plantsList;

            foreach (var item in plantList)
            {
                plantDic.Add(item.PlantType, item.PlantPrefab);
            }
        }

        public GameObject GetPlantPrefabByType(PlantTypeEnum plantType)
        {
            plantDic.TryGetValue(plantType, out GameObject plant);
            // if (plantDic.ContainsKey(plantType)) return plantDic[plantType];
            return plant;
        }
    }
}