using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class AnimationManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    private string animName;
    void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadAnimator();
        LoadSpriteRenderer();
    }
    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
    }
    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public bool CoolDown(string animName, float timer)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animName) && stateInfo.normalizedTime >= timer;
    }

    public Animator Animator
    {
        get { return animator; }
    }
    public SpriteRenderer SpriteRenderer
    {
        get { return spriteRenderer; }
    }

}
