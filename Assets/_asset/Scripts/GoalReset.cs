using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReset : MonoBehaviour
{
    void Start()
    {
        // Reset n về 0 khi bắt đầu game mới
        PlayerPrefs.SetInt("LevelIndex", 0);
        PlayerPrefs.Save();

        Debug.Log("Reset LevelIndex về 0");
    }
}
