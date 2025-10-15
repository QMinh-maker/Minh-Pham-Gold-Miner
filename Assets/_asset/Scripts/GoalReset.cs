using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReset : MonoBehaviour
{
    void Awake()
    {
        // Reset n về 0 khi bắt đầu game mới
        PlayerPrefs.SetInt("LevelIndex", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("StoreLevelNumber", 1);
        Debug.Log("Reset LevelIndex về 1, money ve 0");
    }
}
