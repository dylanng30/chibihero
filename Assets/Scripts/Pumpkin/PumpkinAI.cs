using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinAI : MonoBehaviour
{
    [SerializeField] private PumpkinController controller;


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
        if (controller != null) return;
        controller = GetComponentInParent<PumpkinController>();
    }

    
}
