using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextLevelGoal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requiredScoreText;
    
    
    void Start()
    {
       
        // Tính điểm yêu cầu
        int Goal = PlayerPrefs.GetInt("requiredScore",0);

        // Hiện ra UI
        if (requiredScoreText != null)
        {
            requiredScoreText.text = Goal.ToString();
        }

    }
}
