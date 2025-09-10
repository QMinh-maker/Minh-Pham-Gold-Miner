using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpCash : MonoBehaviour
{
    public Vector2 PopUpSpeed;
    public Rigidbody2D rb;
    public float ShowingTime = 1.5f;
    void Start()
    {
        rb.velocity = PopUpSpeed;
        Destroy(gameObject, ShowingTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
