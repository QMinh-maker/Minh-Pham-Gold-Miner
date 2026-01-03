using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSettings : MonoBehaviour
{
    public GameObject SettingMenu;
    public static bool SettingPress = false;


    // Update is called once per frame
    public void ToggleSetting()
    {
        if (SettingPress)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        SettingMenu.SetActive(false);
        SettingPress = false;       
    }

    public void Pause()
    {
        SettingMenu.SetActive(true);
        SettingPress = true;
    }




}
