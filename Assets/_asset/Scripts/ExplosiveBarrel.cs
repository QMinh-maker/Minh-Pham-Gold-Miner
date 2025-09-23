using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] public float ExplosiveRange ;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask explodableLayerMask;
    [SerializeField] public float destroyDelay = 0.5f;

    private bool hasExploded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook"))
        {
            //Exploded = true;
            Debug.Log("Hook chạm vào TNT!");
            Explode();
            
        }
    }

    private void Explode()
    {
        if (hasExploded) return;   // chặn nổ lại lần 2
        hasExploded = true;

        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        Collider2D[] objectsToBlow = Physics2D.OverlapCircleAll(
            transform.position, ExplosiveRange, explodableLayerMask);

        foreach (var obj in objectsToBlow) 
        {
            if (obj.gameObject == this.gameObject) continue; // bỏ qua chính nó

            // Nếu là TNT khác → gọi Explode()
            ExplosiveBarrel otherTNT = obj.GetComponent<ExplosiveBarrel>();
            if (otherTNT != null)
            {
                otherTNT.Explode();
            }
            else
            {
                // Nếu là item bình thường thì hủy
                Destroy(obj.gameObject);
            }   
        }
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);
    }
}
