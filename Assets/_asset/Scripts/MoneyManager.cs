using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerScore", 0);
        UpdateUI(playerMoney);
    }

    public void AddMoney(int amount)
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerScore", 0);
        playerMoney += amount;
        SaveAndUpdate(playerMoney);
    }

    public bool SpendMoney(int amount)
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerScore", 0);

        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            SaveAndUpdate(playerMoney);
            return true;
        }

        Debug.Log("Không đủ tiền!");
        return false;
    }

    private void SaveAndUpdate(int newValue)
    {
        PlayerPrefs.SetInt("PlayerScore", newValue);
        PlayerPrefs.Save();
        UpdateUI(newValue);
    }

    private void UpdateUI(int value)
    {
        moneyText.text = "$" + value;
    }

}
