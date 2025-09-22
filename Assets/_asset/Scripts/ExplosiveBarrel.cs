using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] public float ExplosiveRange ;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask explodableLayerMask;
    [SerializeField] public float destroyDelay = 0.5f;

    [SerializeField]
    private GameObject barrelRemainsPrefab;

    private bool Exploding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook"))
        {
            Exploding = true;
            Debug.Log("Hook chạm vào TNT!");
            Explode();
            
        }
    }

    private void Explode()
    {
        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        Collider2D[] objectsToBlow = Physics2D.OverlapCircleAll(
            transform.position, ExplosiveRange, explodableLayerMask);

        foreach (var objectToBlow in objectsToBlow) 
        { 
            Destroy(objectToBlow.gameObject);
        }

        if (barrelRemainsPrefab != null)
        {
            Instantiate(barrelRemainsPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);
    }
}
