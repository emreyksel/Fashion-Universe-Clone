using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    public static ObjectPool instance;

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {
        instance = this;

        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();

            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);

                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
            return null;
        
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void SendPooledObject(int objectType,GameObject obj)
    {
        if (objectType >= pools.Length)
            return;

        obj.SetActive(false);
        pools[objectType].pooledObjects.Enqueue(obj);
    }
}
