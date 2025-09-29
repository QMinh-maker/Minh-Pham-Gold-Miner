using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("PlayerScore", 0); // lấy lại điểm, mặc định 0 nếu chưa có
        scoreText.text = "$ " + score.ToString();
    }
}
