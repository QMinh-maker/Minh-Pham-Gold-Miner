using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 60f;                 // Thời gian giới hạn
    public string nextSceneName ;       // Tên scene tiếp theo
    public string gameOverSceneName = "GameOver"; // Tên scene GameOver

    private float timer;
    private int n;                // số lần mở scene
    private int requiredScore;    // điểm cần

    void Start()
    {
        timer = timeLimit;

        // Lấy n từ PlayerPrefs, mặc định 1 nếu chưa có
        n = PlayerPrefs.GetInt("LevelIndex", 0) ;
        PlayerPrefs.SetInt("LevelIndex", n); // lưu lại để lần sau tăng
        PlayerPrefs.Save();

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

        if (playerScore >= requiredScore)
        {
            SceneManager.LoadScene(nextSceneName); // qua màn
        }
        else
        {
            SceneManager.LoadScene(gameOverSceneName); // thua
        }
    }
}
