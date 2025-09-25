using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    private int currentScore;

    void Start()
    {
        // Lấy điểm đã lưu từ PlayerPrefs, mặc định 0
        currentScore = PlayerPrefs.GetInt("PlayerMoney", 0);       
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        PlayerPrefs.SetInt("PlayerMoney", currentScore); // lưu lại để scene sau dùng
        PlayerPrefs.Save();
        Debug.Log("Điểm đã lưu của bạn là: " + currentScore);
    }
}
