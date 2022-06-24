using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockStant : MonoBehaviour
{
    public GameObject stant;
    public TextMeshProUGUI priceText;
    [SerializeField] private int price = 80;

    private void Awake()
    {
        priceText.text = price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && UpgradeManager.instance.money >= price)
        {
            Instantiate(stant, transform.position, stant.transform.rotation);
            UpgradeManager.instance.UpdateMoney(-price);
            Destroy(gameObject);
        }
    }
}
