using System.Collections;
using System.Collections.Generic;
using Characters.Zombies;
using Conf;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ZombieManager:SingletonMono<ZombieManager>
    {
        //this class is responsible for create zombies and zombie heads
        // private Zombie_SO zombieSo;
        private Transform zombieParent;
        private int randomRow;
        
        private readonly List<GameObject> zombies = new List<GameObject>();

        public bool IsZombieCleanUp()
        {
            return zombies.Count <= 0;
        }
        private Vector3 GetRandomPosition()
        {
            randomRow = Random.Range(0, 5);
            float randomX = Random.Range(GridConfig.ZombieSpawnRange.x, GridConfig.ZombieSpawnRange.y);
            Vector3 randomPos = new Vector3(randomX, GridConfig.firstGridPosition.y+0.5f + randomRow*GridConfig.gridSize.y, 0);
            return randomPos;
        }
        public void GenerateHead(Transform parent)
        {
            StartCoroutine(HeadDropping(parent.position,transform));
        }

        public void GenerateZombies(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateZombie(i);
            }
        }

        public void StartInvasion()
        {
            for (int i = 0; i < zombies.Count; i++)
            {
                zombies[i].GetComponent<ZombieBase>().StartInvasion = true;
            }
        }
        private void GenerateZombie(int index)
        {
            // Debug.Log(sortingOrder);
            // GameObject zombie=GameObject.Instantiate(zombieSo.zombieDataList[0].zombiePrefab,GetRandomPosition(),Quaternion.identity,transform);
           
            //there is a potential issue that the type of zombie should be multiple, but here just one type. I think here I should read the configuration files to get the right types
            GameObject zombie = ObjectPoolManager.Instance.GetObject(PoolTypeEnum.NormalZombie, GetRandomPosition(),
                Quaternion.identity);   
            zombies.Add(zombie);
            zombie.GetComponent<SpriteRenderer>().sortingOrder = (4-randomRow*100)+index;
        }
        private IEnumerator HeadDropping(Vector3 position,Transform parent)
        {
            // GameObject head = GameObject.Instantiate(zombieSo.zombieHead,position,Quaternion.identity,parent);
            GameObject head =
                ObjectPoolManager.Instance.GetObject(PoolTypeEnum.ZombieHead, position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
            // Destroy(head);
            ObjectPoolManager.Instance.ReturnObject(PoolTypeEnum.ZombieHead,head);
        }

        public void ReturnZombieToPool(GameObject zombie,PoolTypeEnum poolType)
        {
            zombies.Remove(zombie);
            ObjectPoolManager.Instance.ReturnObject(poolType, zombie);
        }
    }
}