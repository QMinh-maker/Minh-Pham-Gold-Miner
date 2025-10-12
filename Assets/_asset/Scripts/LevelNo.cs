using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelNumberText;
    private int levelNumber;

    void Start()
    {
        levelNumber = PlayerPrefs.GetInt("LevelIndex",1); // mặc định 1 nếu chưa có
        
        PlayerPrefs.SetInt("LevelIndex", levelNumber);
        PlayerPrefs.Save();

        if (LevelNumberText != null)
        {
            LevelNumberText.text = levelNumber.ToString();
        }
    }

   
}
