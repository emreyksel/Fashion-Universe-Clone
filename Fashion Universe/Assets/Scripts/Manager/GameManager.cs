using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public List<GameObject> stants = new List<GameObject>();
    [HideInInspector] public List<GameObject> notEmptyStants = new List<GameObject>();
    [HideInInspector] public List<GameObject> notFullStants = new List<GameObject>();

    private float spawnInterval = 1; 
    [HideInInspector] public int curClient;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        StartCoroutine(nameof(SpawnClient));
    }

    private IEnumerator SpawnClient()
    {
        while (curClient < notEmptyStants.Count) 
        {
            GameObject obj = ObjectPool.instance.GetPooledObject(1);
            curClient++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
