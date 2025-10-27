using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoPause : MonoBehaviour
{
    public void PauseForOneSecond()
    {
        StartCoroutine(PauseCoroutine());
    }

    private IEnumerator PauseCoroutine()
    {
        // Dừng game
        Time.timeScale = 0f;
        Debug.Log("⏸ Game paused!");

        // Chờ 1 giây theo thời gian thực (không bị ảnh hưởng bởi timeScale)
        yield return new WaitForSecondsRealtime(1f);

        // Chạy lại
        Time.timeScale = 1f;
        Debug.Log("▶ Game resumed!");
    }
}
