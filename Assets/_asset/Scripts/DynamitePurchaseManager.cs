using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamitePurchaseManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dynamiteCountText; // UI hiển thị số lần mua
    private int dynamiteBuyCount;

    private const string DYNAMITE_KEY = "DynamiteCount"; // key lưu trong PlayerPrefs

    void Start()
    {
        // Lấy số lần đã mua trước đó (mặc định 0 nếu chưa có)
        dynamiteBuyCount = PlayerPrefs.GetInt(DYNAMITE_KEY, 0);
        UpdateUI();
    }

    // Hàm này được gọi khi người chơi bấm nút "Mua Dynamite"
    public void BuyDynamite()
    {
        dynamiteBuyCount++;
        PlayerPrefs.SetInt(DYNAMITE_KEY, dynamiteBuyCount);
        PlayerPrefs.Save();

        Debug.Log("Người chơi đã mua dynamite lần thứ: " + dynamiteBuyCount);
        UpdateUI();
    }

    // Cập nhật text hiển thị
    private void UpdateUI()
    {
        if (dynamiteCountText != null)
        {
            dynamiteCountText.text = "x" + dynamiteBuyCount.ToString();
        }
    }

}
