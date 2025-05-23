using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
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
        StartCoroutine(SetAnimationCouroutine(animName));
    }
    public IEnumerator SetAnimationCouroutine(string animName)
    {
        yield return new WaitUntil(() => animator != null);
        this.animName = animName;
        this.animator.Play(this.animName);
    }

    public bool IsAnimation(string animName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName);
    }

    public bool FinishAnimation(string animName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName) && stateInfo.normalizedTime >= 1f;
    }

    public bool CoolDown(string animName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName) && stateInfo.normalizedTime >= 10f;
    }
}
