using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockArea : MonoBehaviour
{
    public MeshRenderer plane;
    public Material floorMat;
    public TextMeshProUGUI priceText;
    [SerializeField] private int price = 200;

    private void Awake()
    {
        priceText.text = price.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && UpgradeManager.instance.money >= price)
        {
            UpgradeManager.instance.UpdateMoney(-price);
            plane.material = floorMat;
            Destroy(gameObject);
        }
    }
}
