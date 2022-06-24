using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stant : MonoBehaviour
{
    private List<GameObject> slots = new List<GameObject>();
    [HideInInspector] public int maxCapacity = 4;
    [HideInInspector] public int curCapacity;

    public TextMeshProUGUI capacityText;

    private void Start()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            slots.Add(transform.GetChild(i).gameObject);
        }

        GameManager.instance.stants.Add(gameObject);
    }

    private void Update()
    {
        NotEmptyStant();
        NotFullStant();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            capacityText.text = curCapacity.ToString() + "/" + maxCapacity.ToString(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            capacityText.text = "";
        }
    }

    private void NotEmptyStant()
    {
        if (curCapacity == 0 && GameManager.instance.notEmptyStants.Contains(gameObject))
        {
            GameManager.instance.notEmptyStants.Remove(gameObject);
        }

        if (GameManager.instance.notEmptyStants.Contains(gameObject))
            return;

        if (curCapacity != 0)
        {
            GameManager.instance.notEmptyStants.Add(gameObject);
        }
    }

    private void NotFullStant()
    {
        if (curCapacity == maxCapacity && GameManager.instance.notFullStants.Contains(gameObject))
        {
            GameManager.instance.notFullStants.Remove(gameObject);
        }

        if (GameManager.instance.notFullStants.Contains(gameObject))
            return;

        if (curCapacity != maxCapacity)
        {
            GameManager.instance.notFullStants.Add(gameObject);
        } 
    }
}
