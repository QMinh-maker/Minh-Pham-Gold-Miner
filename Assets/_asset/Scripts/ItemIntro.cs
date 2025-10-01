using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemIntro : MonoBehaviour
{
    [SerializeField] private GameObject ShowIntro;
    [SerializeField] private TextMeshProUGUI HideHello;
    private static GameObject currentIntro = null;
    public void SwitchText()
    {
        // 1. Ẩn đoạn chào ban đầu
        if (HideHello != null) HideHello.gameObject.SetActive(false);

        // 2. Nếu có intro cũ khác đang bật thì tắt nó đi
        if (currentIntro != null && currentIntro != ShowIntro)
        {
            currentIntro.SetActive(false);
        }

        // 3. Hiện intro mới
        if (ShowIntro != null)
        {
            ShowIntro.SetActive(true);
            currentIntro = ShowIntro; // cập nhật intro đang mở
        }
    }
   
}

