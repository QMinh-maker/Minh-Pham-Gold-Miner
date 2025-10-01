using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalesManControl : MonoBehaviour
{
    private Animator animator;
    private static bool hasBoughtItem = false; // kiểm tra đã mua gì chưa

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // gọi khi mua thành công item
    public static void SetBoughtItem()
    {
        hasBoughtItem = true;
    }

    // gọi khi ấn nút NextLevel
    public void OnNextLevelPressed()
    {
        if (hasBoughtItem)
        {
            animator.SetTrigger("Happy");
            Debug.Log("Salesman: Happy!");
        }
        else
        {
            animator.SetTrigger("Sad");
            Debug.Log("Salesman: Sad!");
        }

        // reset cờ cho level sau
        hasBoughtItem = false;

        // load scene sau 3s để kịp xem animation
        Invoke(nameof(LoadNextScene), 3f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Next Goals"); // đổi tên scene phù hợp
    }
}
