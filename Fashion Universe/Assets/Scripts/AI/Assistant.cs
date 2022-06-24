using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Assistant : MonoBehaviour
{
    private List<GameObject> stackBalons = new List<GameObject>();
    private NavMeshAgent agent;
    [HideInInspector] public Vector3 target;
    public GameObject hand;
    private GameObject shop;

    [HideInInspector] public int maxSlot = 1;
    private int curSlot = 0;

    private float timeBetweenCharge = 1;
    private float time;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        shop = GameObject.FindGameObjectWithTag("Shop");
    }

    private void Update()
    {
        if (curSlot == 0)
        {
            if (GameManager.instance.notFullStants.Count != 0)
            {
                target = shop.transform.position;
            }
            else
            {
                target = transform.position;
            }          
        }

        if (target != null)
        {
            agent.SetDestination(target);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stant" && curSlot > 0)  
        {
            PutBalon(other);       
        }
    }

    private void OnTriggerStay(Collider other)
    {
        time += Time.deltaTime;

        if (other.tag == "Shop" && curSlot < maxSlot && time > timeBetweenCharge) 
        {
            GetBalonFromShop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        time = 0;
    }

    private void GetBalonFromShop()
    {
        GameObject cloneBalon = ObjectPool.instance.GetPooledObject(0);

        stackBalons.Add(cloneBalon);
        curSlot++;

        cloneBalon.GetComponent<Balon>().hand = hand.transform;
        cloneBalon.GetComponent<Balon>().rotationIndex = curSlot;

        target = FindStant();

        time = 0;
    }

    private void PutBalon(Collider other)
    {
        Stant stant = other.GetComponent<Stant>();
        if (stant.maxCapacity > stant.curCapacity)
        {
            GameObject lastBalon = stackBalons[curSlot - 1];
            stackBalons.Remove(lastBalon);

            ObjectPool.instance.SendPooledObject(0, lastBalon);
            curSlot--;

            other.transform.GetChild(other.GetComponent<Stant>().curCapacity).gameObject.SetActive(true);
            other.GetComponent<Stant>().curCapacity++;
        }
        else
        {
            target = FindStant();
        }
    }

    private Vector3 FindStant()
    {
        int index = Random.Range(0, GameManager.instance.notFullStants.Count);
        return GameManager.instance.notFullStants[index].transform.position;
    }
}
