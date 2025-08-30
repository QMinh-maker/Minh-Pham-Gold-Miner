using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public float min_Z = -55f, max_Z = 55f;
    public float rotate_speed = 5f;

    private float rotate_angle;
    private bool rotate_right;
    private bool canRotate;

    public float move_speed = 3f;
    private float initial_move_speed;

    public float min_Y = -2.5f;
    private float initial_Y;

    private bool moveDown;
    private RopeRenderer ropeRenderer;

    private void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
    }


    void Start()
    {
        initial_Y = transform.position.y;
        initial_move_speed = move_speed;

        canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        GetInput();
        MoveRope();
    }


    void Rotate ()
    {
        if (!canRotate)
            return;
        if (rotate_right)
        {
            rotate_angle += rotate_speed * Time.deltaTime;
        }
        else
        {
            rotate_angle -= rotate_speed * Time.deltaTime;
        }
        transform.rotation = Quaternion.AngleAxis(rotate_angle, Vector3.forward);

        if (rotate_angle >= max_Z)
        {
            rotate_right = false;
        }
        else if (rotate_angle <= min_Z)
        {
            rotate_right = true;
        }

    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0)) { 
            if (canRotate)
            {
                canRotate = false;
                moveDown = true;
            }
        
        }
    }


    void MoveRope()
    {
        if (canRotate)
            return;
        if (!canRotate)
        {
            Vector3 temp = transform.position;
            if (moveDown)
            {
                temp -= transform.up * Time.deltaTime * move_speed;
            }
            else
            {
                temp += transform.up * Time.deltaTime * move_speed;
            }
            transform.position = temp;

            if (temp.y <= min_Y)
            {
                moveDown = false;
            }
            if (temp.y >= initial_Y)
            {
                canRotate = true;
                ropeRenderer.RenderLine(temp, false);
                move_speed = initial_move_speed;
            }
            ropeRenderer.RenderLine(transform.position, true);

        }


    }

}
