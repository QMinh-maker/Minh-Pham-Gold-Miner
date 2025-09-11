using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public int value;      
    public float weight;

    [Header("Special Item Settings")]
    public bool isSpecialItem = false; // Bật lên nếu là item đặc biệt

    void Start()
    {
        if (isSpecialItem)
        {
            // Random giá trị từ 1 đến 800
            value = Random.Range(1, 801);
            weight = 1f; // Không quan tâm đến cân nặng, gán mặc định
        }
    }
}

