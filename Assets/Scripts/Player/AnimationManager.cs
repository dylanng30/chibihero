using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class AnimationManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [Header("Debug")]
    [SerializeField] private string testAnimationName = "Attack";

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
        
        // Check if animator has a controller and the animation state exists
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning($"[AnimationManager] No AnimatorController assigned to {gameObject.name}!");
            yield break;
        }
        
        // Check if the animation state exists using HasState
        if (!animator.HasState(0, Animator.StringToHash(animName)))
        {
            Debug.LogWarning($"[AnimationManager] Animation state '{animName}' not found in AnimatorController of {gameObject.name}!");
            
            // List available animation clips for reference
            var clips = animator.runtimeAnimatorController.animationClips;
            if (clips.Length > 0)
            {
                Debug.Log($"Available animation clips: {string.Join(", ", System.Array.ConvertAll(clips, clip => clip.name))}");
            }
            
            yield break;
        }
        
        this.animName = animName;
        this.animator.Play(this.animName);
        Debug.Log($"[AnimationManager] Playing animation: {animName}");
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

    // Debug and test methods
    [ContextMenu("Debug Animator States")]
    private void DebugAnimatorStates()
    {
        if (animator == null) LoadAnimator();
        
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning($"[AnimationManager] No AnimatorController assigned to {gameObject.name}!");
            return;
        }

        Debug.Log($"=== Animator States for {gameObject.name} ===");
        
        // List all animation clips
        var clips = animator.runtimeAnimatorController.animationClips;
        Debug.Log($"Animation Clips ({clips.Length}): {string.Join(", ", System.Array.ConvertAll(clips, clip => clip.name))}");
        
        // Test common state names
        string[] commonStates = { "Attack", "Idle", "Walk", "Run", "Jump", "Dash", "Hit", "Die" };
        Debug.Log("=== Testing Common State Names ===");
        foreach (string stateName in commonStates)
        {
            bool hasState = animator.HasState(0, Animator.StringToHash(stateName));
            Debug.Log($"State '{stateName}': {(hasState ? "EXISTS" : "NOT FOUND")}");
        }
    }

    [ContextMenu("Test Animation")]
    private void TestAnimation()
    {
        if (string.IsNullOrEmpty(testAnimationName))
        {
            Debug.LogWarning("[AnimationManager] Test animation name is empty!");
            return;
        }
        
        Debug.Log($"[AnimationManager] Testing animation: {testAnimationName}");
        SetAnimation(testAnimationName);
    }

    [ContextMenu("Get Current Animation Info")]
    private void GetCurrentAnimationInfo()
    {
        if (animator == null) LoadAnimator();
        
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning($"[AnimationManager] No AnimatorController assigned to {gameObject.name}!");
            return;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        
        Debug.Log($"=== Current Animation Info for {gameObject.name} ===");
        Debug.Log($"Current State Hash: {stateInfo.shortNameHash}");
        Debug.Log($"Current State Full Hash: {stateInfo.fullPathHash}");
        Debug.Log($"Normalized Time: {stateInfo.normalizedTime}");
        
        if (clipInfos.Length > 0)
        {
            Debug.Log($"Current Clip: {clipInfos[0].clip.name}");
        }
    }
}
