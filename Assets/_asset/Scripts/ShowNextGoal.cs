using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowNextGoal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requiredScoreText;
    [SerializeField] private float waitTime = 3f; // thời gian chờ trước khi qua scene

    public int n;
    private int requiredScore;

    void Start()
    {
        // Lấy n hiện tại từ PlayerPrefs
        //n = PlayerPrefs.GetInt("LevelIndex",0); mặc định 1 nếu chưa có

        // Tăng n thêm 1 mỗi lần scene này được mở
        n += 1;
        PlayerPrefs.SetInt("LevelIndex", n);
        PlayerPrefs.Save();

        // Tính điểm yêu cầu
        requiredScore = 135 * (n-1) * (n - 1) + 140 * (n - 1) + 375;

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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
