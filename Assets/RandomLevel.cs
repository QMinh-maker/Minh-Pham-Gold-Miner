using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevel : MonoBehaviour
{
    private float timeLimit = 61f;                 // Thời gian giới hạn    
    private float timer;
    private int n;                // số màn hiện tại
    private int requiredScore;    // điểm cần

    void Start()
    {
        timer = timeLimit;

        // Lấy n từ PlayerPrefs, mặc định 2 nếu chưa có
        n = PlayerPrefs.GetInt("LevelIndex", 1);


        // Tính điểm yêu cầu theo công thức
        requiredScore = 135 * n * n + 140 * n + 375;

        Debug.Log($"Level {n}, Required Score = {requiredScore}");
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            CheckScoreAndLoadScene();
        }
    }

    void CheckScoreAndLoadScene()
    {
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int x = Random.Range(1, 16);

        if (playerScore >= requiredScore)
        {
            int nextLevel = n + 1;
            PlayerPrefs.SetInt("LevelIndex", nextLevel);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Level " + x.ToString()); // qua màn
        }
        else
        {
            SceneManager.LoadScene("GameOver"); // thua
        }
    }
}
