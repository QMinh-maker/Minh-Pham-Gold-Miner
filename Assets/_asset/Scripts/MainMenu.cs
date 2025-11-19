using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Nếu scene chơi pause → giữ game paused
        bool wasPaused = PlayerPrefs.GetInt("PausedState", 0) == 1;

        if (wasPaused)
        {
            Time.timeScale = 0f;  // vẫn giữ paused
        }
        else
        {
            Time.timeScale = 1f;  // bình thường
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ATestLevel");
    }
}
