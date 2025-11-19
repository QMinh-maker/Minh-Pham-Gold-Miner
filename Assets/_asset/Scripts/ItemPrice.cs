using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemPrice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private TextMeshProUGUI Money;
    [SerializeField] private TextMeshProUGUI NextLevelTalk;
    [SerializeField] private GameObject ItemIntro;
    [SerializeField] private GameObject DeleteItem;
    

    private int price;
    private bool isInitialized = false;
    private MoneyManager moneyManager;
    
    void Start()
    {
        

        if (!isInitialized)
        {
            price = Random.Range(1, 801);
            //price = 0;
            isInitialized = true;
        }

        PriceText.text = "$" + price;
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void BuyItem()
    {
        if (moneyManager != null)
        {
            bool bought = moneyManager.SpendMoney(price);
            if (bought)
            {
                Debug.Log("Mua thành công: -" + price);               
            }

            if (DeleteItem != null)
            {
                DeleteItem.SetActive(false); // xóa item khi mua thành công
                NextLevelTalk.gameObject.SetActive(true);
                ItemIntro.gameObject.SetActive(false);
                SalesManControl.SetBoughtItem();
            }
        }
        else
        {
            Debug.Log("Không đủ tiền!");
        }
    }
}
