using System.Collections.Generic;
using Conf;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PoolDataSO", menuName = "SO/PoolData")]
    public class PoolDataSO : ScriptableObject
    {
        public List<Pool> pools;
    }
    [System.Serializable]
    public class Pool
    {
        //in the future, It would be replaced by json files
        public PoolTypeEnum poolName;
        public int poolSize;
        public GameObject prefab;
    }
}