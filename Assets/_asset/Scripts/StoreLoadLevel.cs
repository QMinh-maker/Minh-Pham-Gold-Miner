using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreLoadLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelNumberText;

    void Start()
    {
        // Lấy giá trị hiện tại của StoreLevelNumber (mặc định = 1 nếu chưa có)
        int StoreLevelNumber = PlayerPrefs.GetInt("LevelIndex");

        // Nếu đây là lần đầu, đặt bắt đầu từ 2
        if (StoreLevelNumber < 2)
            StoreLevelNumber = 2;
        else
            StoreLevelNumber++; // tăng thêm 1 mỗi lần mở scene

        // Lưu lại để các scene sau có thể dùng
        PlayerPrefs.SetInt("LevelIndex", StoreLevelNumber);
        PlayerPrefs.Save();

        // Cập nhật UI
        if (LevelNumberText != null)
        {
            LevelNumberText.text = StoreLevelNumber.ToString();
        }

        Debug.Log($"Store Level: {StoreLevelNumber}");
    }
}

