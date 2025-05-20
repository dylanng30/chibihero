using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadAnimator();
    }
    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(string animName)
    {
        switch (animName)
        {
            case "Idle":
                animator.Play("Idle");
                break;
            case "Run":
                animator.Play("Run");
                break;
            case "NormalATK":
                animator.Play("NormalATK");
                break;
            case "Skil1":
                animator.Play("Skil1l");
                break;
        }
    }
}
