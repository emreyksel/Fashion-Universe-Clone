using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kasa : MonoBehaviour
{
    public static Kasa instance;

    [HideInInspector] public List<GameObject> waitingClient = new List<GameObject>();
    public Transform[] payAreas = new Transform[6]; 
    private Vector3 endPoint;

    public GameObject money;
    public Transform panel;

    private void Awake()
    {
        instance = this;

        endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Client")
        {
            waitingClient.Add(other.gameObject);
        }

        if (other.tag == "Player")
        {
            foreach (GameObject client in waitingClient)
            {
                GameObject cloneMoney = ObjectPool.instance.GetPooledObject(2);
                cloneMoney.transform.SetParent(panel);
                cloneMoney.transform.position = Camera.main.WorldToScreenPoint(client.transform.position + Vector3.up);
                
                client.GetComponent<Client>().target = endPoint;
            }

            waitingClient.Clear();
        }
    }
}
