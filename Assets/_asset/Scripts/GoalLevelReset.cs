using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLevelReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("LevelForScore", 1);
        PlayerPrefs.Save();
    }

   
}
