using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Client : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject cloneBalon;
    public GameObject hand;
    private Vector3 startPoint; 
    private Vector3 endPoint;   
    [HideInInspector] public Vector3 target;
    private static int waitingNumber;
    private int counter;
    private bool haveBalon = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
    }

    private void OnEnable()
    {
        transform.position = startPoint;
        transform.eulerAngles = new Vector3(0, 180, 0);

        if (counter>0)
        {
            target = FindStant();
        }
        
        counter++;
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target);
        }
        
        SetAnimatoins();
    }

    private void SetAnimatoins()
    {
        if (agent.velocity.magnitude > 0.5f)
        {
            anim.SetFloat("Velocity", agent.velocity.magnitude);
        }
        else
        {
            anim.SetFloat("Velocity", agent.velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Stant" && !haveBalon)
        {
            StartCoroutine(GetBalon(other));
        }

        else if (other.tag == "EndPoint")
        {
            GameManager.instance.curClient--;
            waitingNumber--;
            haveBalon = false;
            ObjectPool.instance.SendPooledObject(0, cloneBalon);
            ObjectPool.instance.SendPooledObject(1, gameObject);           
        }
    }

    private IEnumerator GetBalon(Collider other)
    {
        yield return new WaitForSeconds(1);

        if (other.GetComponent<Stant>().curCapacity != 0) 
        {
            cloneBalon = ObjectPool.instance.GetPooledObject(0);
            cloneBalon.GetComponent<Balon>().hand = hand.transform;
            cloneBalon.GetComponent<Balon>().rotationIndex = 1;
            haveBalon = true;

            other.transform.GetChild(other.GetComponent<Stant>().curCapacity - 1).gameObject.SetActive(false);
            other.GetComponent<Stant>().curCapacity--;

            target = Kasa.instance.payAreas[waitingNumber].position;
            waitingNumber++;
        }
        else
        {
            target = FindStant();
        }
    }

    private Vector3 FindStant()
    {
        int index = Random.Range(0, GameManager.instance.notEmptyStants.Count);
        return GameManager.instance.notEmptyStants[index].transform.position;
    }
}
