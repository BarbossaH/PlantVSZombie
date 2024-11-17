using System;
using System.Collections;
using Conf;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ZombieManager:SingletonMono<ZombieManager>
    {
        //this class is responsible for create zombies and zombie heads
        private Zombie_SO zombieSo;
        private Transform zombieParent;
        protected override void Init()
        {
            base.Init();
            zombieSo = Resources.Load<Zombie_SO>("Zombie_SO");
           // zombieParent=GameObject.Find("")
        }

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                GenerateBody();
            }
        }

        private Vector3 GetRandomPosition()
        {
            int randomRow = Random.Range(0, 5);
            float randomX = Random.Range(GridConfig.ZombieSpawnRange.x, GridConfig.ZombieSpawnRange.y);
            Vector3 randomPos = new Vector3(randomX, GridConfig.firstGridPosition.y+0.5f + randomRow*GridConfig.gridSize.y, 0);
            return randomPos;
        }
        public void GenerateHead(Transform parent)
        {
            StartCoroutine(HeadDropping(parent.position,transform));
        }

        public void GenerateBody()
        {
            GameObject zombie=GameObject.Instantiate(zombieSo.zombieDataList[0].zombiePrefab,GetRandomPosition(),Quaternion.identity,transform);
        }
        private IEnumerator HeadDropping(Vector3 position,Transform parent)
        {
            GameObject head = GameObject.Instantiate(zombieSo.zombieHead,position,Quaternion.identity,parent);
            yield return new WaitForSeconds(1.0f);
            Destroy(head);
        }
    }
}