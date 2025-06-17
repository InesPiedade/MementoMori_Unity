using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkVisionAbility : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Timer()
    {
        animator.SetTrigger("Timer");
    }
}
