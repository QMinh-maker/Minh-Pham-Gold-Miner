using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public float min_Z = -55f, max_Z = 55f; // Giới hạn góc lắc
    public float rotate_speed = 5f;         // Tốc độ lắc qua lại
    private float rotate_angle;             // Góc hiện tại
    private bool rotate_right;              // Đang quay sang phải hay trái
    public bool canRotate;                 // Có được lắc nữa không

    public float move_speed = 3f;           // Tốc độ di chuyển móc
    public float initial_move_speed;       // Lưu lại tốc độ ban đầu

   
    private float initial_Y;                // Vị trí Y ban đầu (mốc reset)

    public bool moveDown;                  // Trạng thái: đang đi xuống
    private RopeRenderer ropeRenderer;      // Script vẽ dây

    public float maxRopeLength ;            // Chiều dài dây tối đa
    private Vector3 startPos;               // Điểm neo ban đầu của dây

    private Hook hook;
    private GameObject currentItem;

    

    private void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
        hook = FindObjectOfType<Hook>(); // tìm script Hook trong scene
    }

    void Start()
    {
        initial_Y = transform.position.y;
        initial_move_speed = move_speed;
        canRotate = true;
        startPos = transform.position;

        
    }

    void Update()
    {
        
        Rotate(); //lắc dây qua lại khi đang nghỉ
        GetInput();//kiểm tra xem người chơi có thả dây ko
        MoveRope();//xử lý di chuyển lên xuống của móc
        CheckItemDestroyed();//kiểm tra item bị destroy
    }

    void Rotate()
    {
        if (!canRotate)
            return;
        //Nếu canRotate == true → móc lắc qua lại giữa min_Z và max_Z.

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
        //Đảo chiều khi chạm biên.
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canRotate)
            {
                canRotate = false;
                moveDown = true;
                //Khi bấm chuột trái: Nếu đang ở trạng thái nghỉ
                //(có thể lắc) sẽ ngừng lắc(canRotate = false) khi
                //bắt đầu thả xuống
               
            }
        }
    }

    void MoveRope()
    {
        if (canRotate)
            return;

        Vector3 temp = transform.position;

        if (moveDown)
        {
            temp -= transform.up * Time.deltaTime * move_speed;
            //Nếu moveDown = true → móc đi xuống theo hướng -transform.up

            // Kiểm tra độ dài dây
            float distance = Vector3.Distance(startPos, temp);
            if (distance >= maxRopeLength)
            {
                moveDown = false;
                //Khi độ dài dây vượt quá maxRopeLength, đổi sang moveDown = false để kéo lên.
                // Không cho bắt item khi dây chạm max length
                if (hook != null)
                    hook.DisableCatch();
                
            }
        }
        else
        {
            temp += transform.up * Time.deltaTime * move_speed;
        }

        transform.position = temp;

        // Nếu dây về tới vị trí ban đầu thì reset
        if (temp.y >= initial_Y)
        {
            canRotate = true;
            ropeRenderer.RenderLine(temp, false);
            move_speed = initial_move_speed;
            // Cho phép bắt item lại khi về Miner
            if (hook != null)
                hook.EnableCatch();
            
        }
        else
        {
            ropeRenderer.RenderLine(transform.position, true); // vẽ dây
        }
    }


    public void HandleMoveBackOnHittingItem(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            moveDown = false; // bắt đầu kéo lên
            ropeRenderer.RenderLine(transform.position, false); // tắt vẽ dây ngay lập tức
            currentItem = other.gameObject; // lưu lại item
            //Debug.Log("Kéo Item lên");

            
        }
    }

    public void ApplyWeight(float weight)
    {
        move_speed = move_speed / weight;
        
    }

    public void ResetMoveSpeed()
    {
        move_speed = initial_move_speed;
    }

     private void CheckItemDestroyed()
    {
        // Nếu item đang bị kéo mà bị destroy => khôi phục tốc độ
        if (currentItem == null && move_speed != initial_move_speed)
        {
            ResetMoveSpeed();
            //Debug.Log("Item bị destroy, khôi phục tốc độ ban đầu");
        }
    }

   

}
