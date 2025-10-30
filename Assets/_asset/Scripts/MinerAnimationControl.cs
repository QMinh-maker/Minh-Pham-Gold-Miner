using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAnimationControl : MonoBehaviour
{
    private Animator animator;
    private HookMovement hookMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        hookMovement = FindObjectOfType<HookMovement>();
    }

    void Update()
    {
        if (hookMovement == null) return;

        // trạng thái idle → canRotate = true
        bool isIdle = hookMovement.IsRotating();

        // trạng thái dig → đang đi xuống
        bool isDig = hookMovement.IsMovingDown();

        // trạng thái pull → đang đi lên (không rotate, không moveDown)
        bool isPull = (!hookMovement.IsRotating() && !hookMovement.IsMovingDown());

        // cập nhật Animator
        animator.SetBool("IsIdle", isIdle);
        animator.SetBool("IsDig", isDig);
        animator.SetBool("IsPull", isPull);
    }
}

