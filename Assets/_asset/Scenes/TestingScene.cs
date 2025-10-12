using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingScene : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("explosive test");
    }
}
