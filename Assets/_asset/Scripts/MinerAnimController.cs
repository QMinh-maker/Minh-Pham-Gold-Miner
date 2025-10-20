using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAnimController : MonoBehaviour
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

        // Trạng thái Idle (lắc dây)
        if (hookMovement.canRotate == true)
        {
            animator.Play("miner-wait");
        }
        // Trạng thái Thả dây
        else if (hookMovement.moveDown == true)
        {
            animator.Play("miner-dig");
        }
        // Trạng thái Kéo lên
        else
        {
            animator.Play("miner-pull");
        }
    }
}


