using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplode : MonoBehaviour
{
    private Animator animator;
    private bool isExploding = false;

    [SerializeField] private string itemTag = "Item"; // Tag của các vật thể (vàng, đá,...)

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu đã nổ rồi thì bỏ qua
        if (isExploding) return;

        // Chỉ nổ khi chạm vào vật thể có tag "Item"
        if (collision.CompareTag(itemTag))
        {
            Explode();
            Destroy(collision.gameObject); // Huỷ vật bị trúng nổ
        }
    }

    private void Explode()
    {
        isExploding = true;
        Debug.Log("Dynamite exploded!");

        // Gọi animation nổ
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        
        // Huỷ chính Dynamite sau khi nổ (trễ một chút để kịp chạy animation)
        Destroy(gameObject, 0.5f);
    }
}


