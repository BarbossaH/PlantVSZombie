using Conf;

namespace Managers
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PlantManager : SingletonMono<PlantManager>
    {
        //this class is for managing the data of all plants, via this class, I can get any plants I want due to the plant type
        private readonly Dictionary<PoolTypeEnum, GameObject> plantDic = new Dictionary<PoolTypeEnum, GameObject>();
        private readonly Dictionary<PoolTypeEnum, GameObject> bulletDic = new Dictionary<PoolTypeEnum, GameObject>();
        private readonly Dictionary<PoolTypeEnum, Sprite> bulletHitDic = new Dictionary<PoolTypeEnum, Sprite>();
        protected override void Init()
        {
            PlantListSO plantListSO = Resources.Load<PlantListSO>("PlantListSO");
            Bullet_SO bullet_SO = Resources.Load<Bullet_SO>("Bullet_SO");
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

            List<BulletData> bulletList = bullet_SO.bulletList;
            foreach (var bullet in bulletList)
            {
                bulletDic.Add(bullet.ButtetType, bullet.BulletPrefab);
                bulletHitDic.Add(bullet.ButtetType,bullet.BulletHitSprite);
            }
        }

        public GameObject GetPlantPrefabByType(PoolTypeEnum plantType)
        {
            plantDic.TryGetValue(plantType, out GameObject plant);
            // if (plantDic.ContainsKey(plantType)) return plantDic[plantType];
            return plant;
        }

        public GameObject GetBulletPrefabByType(PoolTypeEnum bulletType)
        {
            bulletDic.TryGetValue(bulletType, out GameObject bullet);
            return bullet;
        }
        public Sprite GetBulletHitSpriteByType(PoolTypeEnum bulletType)
        {
            bulletHitDic.TryGetValue(bulletType, out Sprite bullet);
            return bullet;
        }
    }
}