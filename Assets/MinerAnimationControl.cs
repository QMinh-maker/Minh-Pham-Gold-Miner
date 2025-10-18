using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAnimationControl : MonoBehaviour
{
    public static MinerAnimationControl Instance; // Singleton để gọi từ script khác
    private Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        animator.Play("Idle");
    }
   
    public void PlayPull()
    {
        animator.SetTrigger("Pull");
    }

    public void PlayDig()
    {
        animator.SetTrigger("Dig");
    }

    public void PlayThrow()
    {
        animator.SetTrigger("Throw");
    }

    public void PlayBuf()
    {
        animator.SetTrigger("Buff");
    }

}
