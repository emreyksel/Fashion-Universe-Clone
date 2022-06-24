using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    private Transform moneyImage;
    [SerializeField] private Ease easeType;
    private const int value = 80;
    private int counter;

    private void Awake()
    {
        moneyImage = GameObject.FindGameObjectWithTag("MoneyImage").transform;
    }

    private void OnEnable()
    {
        if (counter >0)
        {
            StartCoroutine(nameof(MoneyMove));
        }
        counter++;       
    }

    private IEnumerator MoneyMove()
    {
        transform.DOMove(moneyImage.position, 2).SetEase(easeType).OnComplete(() =>
        {
            UpgradeManager.instance.UpdateMoney(value);
            ObjectPool.instance.SendPooledObject(2, gameObject);
        });
        yield return null;
    }
}
