using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMouse : MonoBehaviour
    
{
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    public float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning",true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(Speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-Speed, 0);
        }

        if(Vector2.Distance(transform.position,currentPoint.position) < 50f
            && currentPoint == pointB.transform) 
        {
            flip();
            currentPoint = pointA.transform;
        }
            
        if(Vector2.Distance(transform.position,currentPoint.position) < 50f
            && currentPoint == pointA.transform) 
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 50f);
        Gizmos.DrawWireSphere(pointB.transform.position, 50f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

    }
}
