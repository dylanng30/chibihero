using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KingAI : MonoBehaviour
{
    [SerializeField] private KingController kingController;

    private IState stateBeforeHit;

    protected virtual void Awake()
    {
        LoadComponent();

    }
    protected virtual void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (kingController != null) return;
        kingController = GetComponentInParent<KingController>();
    }

    public void SetStateBeforeHit(IState stateBeforeHit)
    {
        this.stateBeforeHit = stateBeforeHit;
    }
    public IState StateBeforeHit
    { 
        get { return stateBeforeHit; }
    }

}
