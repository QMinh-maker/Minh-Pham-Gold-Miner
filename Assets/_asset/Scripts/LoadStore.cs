using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStore : MonoBehaviour
{
    public void OpenStore()
    {
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1; // lấy scene cuối cùng
        SceneManager.LoadScene(lastSceneIndex);
    }
}
