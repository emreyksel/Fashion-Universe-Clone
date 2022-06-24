using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balon : MonoBehaviour
{
    [HideInInspector] public Transform hand;
    [HideInInspector] public int rotationIndex;

    void Update()
    {
        if (hand != null)
        {
            transform.position = hand.position;

            switch (rotationIndex)
            {
                case 1:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 2:
                    transform.eulerAngles = new Vector3(0, 0, 15);
                    break;
                case 3:
                    transform.eulerAngles = new Vector3(0, 0, -15);
                    break;
                case 4:
                    transform.eulerAngles = new Vector3(0, 0, -30);
                    break;
                case 5:
                    transform.eulerAngles = new Vector3(0, 0, -30);
                    break;
            }
        }
    }
}
