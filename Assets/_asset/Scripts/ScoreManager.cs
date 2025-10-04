using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    private int currentScore;

    void Awake()
    {
        // Lấy điểm đã lưu từ PlayerPrefs, mặc định 0
        currentScore = PlayerPrefs.GetInt("PlayerScore", 0); 
        SaveScore();
    }

    // Hàm cộng điểm
    public void AddScore(int amount)
    {
        currentScore += amount;
        SaveScore();
    }

    // Hàm trừ điểm
    public void SubtractScore(int amount)
    {
        currentScore -= amount;
        if (currentScore < 0) currentScore = 0;
        SaveScore();
    }

    // Hàm set trực tiếp điểm
    public void SetScore(int newScore)
    {
        currentScore = Mathf.Max(0, newScore);
        SaveScore();
    }

    // Hàm lấy điểm hiện tại
    public int GetScore()
    {
        return currentScore;
    }

    // Bất kỳ thay đổi nào cũng lưu ngay
    private void SaveScore()
    {
        PlayerPrefs.SetInt("PlayerScore", currentScore);
        PlayerPrefs.Save();
        Debug.Log("Điểm đã lưu của bạn là: " + currentScore);
    }
}
