using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public GameObject assistant;

    public Image upgradePanel;

    public Button[] buttons = new Button[4];
    public Button assistantButton;
    public Button increaseCapacityButton;
    public Button reduceChargeSpeedButton;
    public Button increaseAssistantSpeedButton;

    public TextMeshProUGUI moneyText;
    [HideInInspector] public int money;

    private int assistantValue = 300;
    private int increaseCapacityValue = 50;
    private int reduceChargeSpeedValue = 200;
    private int increaseAssistantSpeedValue = 100;


    private void Awake()
    {
        instance = this;
    }

    public void UpdateMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();
    }

    public void ExitButton()
    {
        upgradePanel.gameObject.SetActive(false);
    }

    public void AssistantButton()
    {
        Instantiate(assistant, Vector3.zero, Quaternion.identity);

        UpdatePanel(assistantValue, assistantButton);
    }

    public void IncreaseCapacity()
    {
        PlayerTrigger.instance.maxSlot++;

        UpdatePanel(increaseCapacityValue, increaseCapacityButton);
    }

    public void ReduceChargeSpeed()
    {
        PlayerTrigger.instance.timeBetweenCharge -= 0.5f;

        UpdatePanel(reduceChargeSpeedValue, reduceChargeSpeedButton);
    }

    public void IncreaseAssistantSpeed()
    {
        GameObject[] assistants = GameObject.FindGameObjectsWithTag("Assistant");
        foreach (var assistant in assistants)
        {
            assistant.GetComponent<NavMeshAgent>().speed += 0.5f;
        }

        UpdatePanel(increaseAssistantSpeedValue, increaseAssistantSpeedButton);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            upgradePanel.gameObject.SetActive(true);

            foreach (var button in buttons)
            {
                int.TryParse(button.GetComponentInChildren<Text>().text, out int value);

                if (value > money)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
    }

    public void UpdatePanel(int price,Button buton)
    {
        UpdateMoney(-price);
        price += 200;
        buton.GetComponentInChildren<Text>().text = price.ToString();

        foreach (var button in buttons)
        {
            int.TryParse(button.GetComponentInChildren<Text>().text, out int value);
 
            if (value > money)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            upgradePanel.gameObject.SetActive(false);
        }
    }
}
