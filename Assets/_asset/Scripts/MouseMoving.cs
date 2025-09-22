using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseMoving : MonoBehaviour
{
    
    public Transform PointA;
    public Transform PointB;

    private Rigidbody2D rb;
    private Animator anim;
    
    private Transform targetPoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        targetPoint = PointB;
        anim.SetBool("isRunning",true);
    }

    // Update is called once per frame
    void Update()
    {       
        // Di chuyển tới điểm hiện tại
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, 0);

        // Nếu đã gần targetPoint
        if (Vector2.Distance(transform.position, targetPoint.position) < 50f)
        {
            // Đổi target
            targetPoint = (targetPoint == PointA) ? PointB : PointA;
            
            // Flip hướng
            Flip();
        }
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        Debug.Log("Quay lại");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 50f);
        Gizmos.DrawWireSphere(PointB.transform.position, 50f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
