using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStore : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f;

    void Start()
    {
        Invoke(nameof(OpenStore), waitTime);
    }

    public void OpenStore()
    {
       
        SceneManager.LoadScene("Store");
    }
}
