using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStore : MonoBehaviour
{
    public string nextSceneName;
    public void OpenStore()
    {
       
        SceneManager.LoadScene(nextSceneName);
    }
}
