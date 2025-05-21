using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    private string animName;
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
        this.animName = animName;
        animator.Play(animName);
    }

    public bool IsAnimation(string animName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName);
    }

    public bool FininshAnimation(string animName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName) && stateInfo.normalizedTime >= 1f;
    }
}
