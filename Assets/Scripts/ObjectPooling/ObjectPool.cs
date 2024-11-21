using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool
    {
        private readonly Queue<GameObject> poolQueue=new Queue<GameObject>();
        private readonly GameObject prefab;
        private readonly Transform parent;

        public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
        {
            //using constructor function to initialize the member variables, so that this class can be used for each object that needs an object pool
            this.prefab=prefab;
            this.parent = parent;
            //initialize the game object pool
            InitObjectPool(initialSize);
        }

        private void InitObjectPool(int initialSize)
        {
            for (int i = 0; i<initialSize; i++)
            {
                // Debug.Log(i);
                var obj = CreateObject();
                ReturnObject(obj);
            }
        }

        private GameObject CreateObject()
        {
            var obj=Object.Instantiate(prefab);
            return obj;
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            var obj = poolQueue.Count >0?poolQueue.Dequeue() : CreateObject();
            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.transform.SetParent(null);
            // Debug.Log(111);
            return obj;
        }

        public void ReturnObject(GameObject obj)
        {
            //maybe here should reset the position and rotation, maybe not, because it already set invisible, so it doesn't matter
            if (parent is not null)
            {
                obj.transform.SetParent(parent);
            }
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }
}