using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPrice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PriceText;
    private int price;
    private bool isInitialized = false;
    void Start()
    {
        if (!isInitialized)
        {
            price = Random.Range(1, 801); // random một lần
            isInitialized = true;
        }

        PriceText.text = "$" + price;
    }
}
