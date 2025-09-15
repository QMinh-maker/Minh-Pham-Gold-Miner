using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoading : MonoBehaviour
{
    [SerializeField] private float delay = 3f;   // Thời gian chờ (3 giây)
    [SerializeField] private string nextSceneName; // Index của scene kế tiếp (hoặc thay bằng tên)
    void Start()
    {
        // Gọi hàm LoadNextScene sau "delay" giây
        Invoke(nameof(LoadNextScene), delay);
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Chưa gán tên Scene kế tiếp trong Inspector!");
        }
    }
}
