using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplode : MonoBehaviour
{
    private Animator animator;
    private bool isExploding = false;

    [SerializeField] private string dynamiteTag = "Hook"; // Tag của Dynamite

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        // Nếu đã nổ rồi thì bỏ qua
        if (isExploding) return;
        
        // Chỉ nổ khi va chạm với object có tag Dynamite
        if (collision.CompareTag(dynamiteTag))
        {
            
            Explode();
            
        }
    }

    private void Explode()
    {
        isExploding = true;
        Debug.Log("Explode");

        // Gọi animation nổ
        if (animator != null)
        {
            animator.SetTrigger("Explode");

        }

        // Huỷ object sau khi animation nổ xong
        
        Destroy(gameObject, 0);
        Destroy(GameObject.FindGameObjectWithTag(dynamiteTag));
    }

   
}

