using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTrigger : MonoBehaviour
{
    public static PlayerTrigger instance;

    private List<GameObject> stackBalons = new List<GameObject>();

    public GameObject hand;

    [HideInInspector] public int maxSlot = 3;
    private int curSlot = 0;

    [HideInInspector] public float timeBetweenCharge = 1;
    private float time;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shop")
        {
            other.GetComponentInChildren<TextMeshProUGUI>().text = curSlot.ToString() + "/" + maxSlot.ToString();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        time += Time.deltaTime;

        if (other.tag == "Shop" && curSlot < maxSlot && time > timeBetweenCharge) 
        {
            GetBalonFromShop(other);
        }

        else if (other.tag == "Stant" && curSlot > 0 && other.GetComponent<Stant>().maxCapacity > other.GetComponent<Stant>().curCapacity) 
        {
            PutBalon(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            other.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        time = 0;
    }

    private void GetBalonFromShop(Collider other)
    {
        GameObject cloneBalon = ObjectPool.instance.GetPooledObject(0);

        stackBalons.Add(cloneBalon);
        curSlot++;

        other.GetComponentInChildren<TextMeshProUGUI>().text = curSlot.ToString() + "/" + maxSlot.ToString();

        cloneBalon.GetComponent<Balon>().hand = hand.transform;
        cloneBalon.GetComponent<Balon>().rotationIndex = curSlot;

        time = 0;
    }

    private void PutBalon(Collider other)
    {
        GameObject lastBalon = stackBalons[curSlot - 1];
        stackBalons.Remove(lastBalon);

        ObjectPool.instance.SendPooledObject(0, lastBalon);
        curSlot--;

        other.transform.GetChild(other.GetComponent<Stant>().curCapacity).gameObject.SetActive(true);
        other.GetComponent<Stant>().curCapacity++;
    }
}
