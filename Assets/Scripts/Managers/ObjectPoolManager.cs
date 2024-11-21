using System.Collections.Generic;
using Conf;
using Data;
using ObjectPooling;
using UnityEngine;

namespace Managers
{
    public class ObjectPoolManager : SingletonMono<ObjectPoolManager> 
    {
        private readonly Dictionary<PoolTypeEnum , ObjectPool> poolsDic = new Dictionary<PoolTypeEnum, ObjectPool>();

        //I cannot use Init to initialize the manager class because the pool could load a very large object, which could cause the program slow.
        //but in this project, the pool won't be very large, so I just use one manager to manager it
        protected override void Init()
        {
            base.Init();
            PoolDataSO poolData = Resources.Load<PoolDataSO>("PoolDataSO");
            if (poolData is not null )
            {
                var pools = poolData.pools; 
                if(pools is null || pools.Count <= 0) return;
                for (int i = 0; i < poolData.pools.Count; i++)
                {
                    Transform parent = transform.Find($"{pools[i].poolName}");
                    if (parent is null)
                    {
                        GameObject parentObject = new GameObject($"{pools[i].poolName}");
                        parentObject.transform.SetParent(transform);
                        parent=parentObject.transform;
                    }

                    // Debug.Log(pools[i].poolSize);
                    CreatePool(pools[i].poolName, pools[i].prefab, pools[i].poolSize,parent);
                }
            }
        }

        public void CreatePool(PoolTypeEnum plantType, GameObject prefab, int initialSize, Transform parent=null)
        {
            if (!poolsDic.ContainsKey(plantType))
            {
                poolsDic.Add(plantType, new ObjectPool(prefab,initialSize, parent));
            }
        }

        public GameObject GetObject(PoolTypeEnum plantType, Vector3 position, Quaternion rotation)
        {
            if (poolsDic.TryGetValue(plantType, out var pool))
            {
                return pool.GetObject(position, rotation);
            }

            // Debug.LogWarning($"Pool with name {poolName} not exist");
            return null;
        }
      public void ReturnObject(PoolTypeEnum plantType, GameObject obj)
      {
          if (poolsDic.TryGetValue(plantType, out var pool))
          {
              pool.ReturnObject(obj);
          }
          else
          {
              // Debug.LogWarning($"Pool with name {poolName} does not exist.");
          }    
          
      }
    }
}