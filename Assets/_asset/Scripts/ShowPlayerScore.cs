using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPlayerScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        int savedScore = PlayerPrefs.GetInt("PlayerMoney", 0); // lấy score đã lưu
        if (scoreText != null)
        {
            scoreText.text = savedScore.ToString();
        }
    }
}
