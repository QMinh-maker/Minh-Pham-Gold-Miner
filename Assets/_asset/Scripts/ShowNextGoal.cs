using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowNextGoal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requiredScoreText;
    [SerializeField] private float waitTime = 3f; // thời gian chờ trước khi qua scene

    
    private int requiredScore;

    void Start()
    {
        // Lấy n hiện tại từ PlayerPrefs
        int n = PlayerPrefs.GetInt("LevelIndex", 1); //mặc định 1 nếu chưa có
        PlayerPrefs.SetInt("LevelIndex",n);
        PlayerPrefs.Save();

        // Tính điểm yêu cầu
        requiredScore = 135 * n * n  + 140 * n + 375;

        // Hiện ra UI
        if (requiredScoreText != null)
        {
            requiredScoreText.text = "$" + requiredScore.ToString();
            PlayerPrefs.SetInt("requiredScore", requiredScore);
            PlayerPrefs.Save();
        }

        // Sau waitTime giây thì load scene kế tiếp
        Invoke(nameof(LoadNextScene), waitTime);
    }

    void LoadNextScene()
    {
        int n = PlayerPrefs.GetInt("LevelIndex", 1);
        string nextSceneName;

        if (n <= 15)
        {
            // Nếu n <= 15 → load đúng scene tên "Level n"
            nextSceneName = "Level " + n;
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Nếu n > 15 → load ngẫu nhiên scene "LevelX" (X từ 1 đến 15)
            int randomLevel = Random.Range(1, 16);
            nextSceneName = "Level " + randomLevel;
            SceneManager.LoadScene(nextSceneName);
        }


    }
}
