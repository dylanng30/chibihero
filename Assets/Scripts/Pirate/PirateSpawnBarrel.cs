using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawnBarrel : MonoBehaviour
{
    [SerializeField] private PirateController pirateController;
    [SerializeField] private GameObject BarrelPrefab;

    private void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        LoadController();
    }
    protected virtual void LoadController()
    {
        if (this.pirateController != null)
            return;
        this.pirateController = GetComponentInParent<PirateController>();
    }
    public void Spawn()
    {
        Instantiate(BarrelPrefab, this.transform.position, Quaternion.identity);
    }

}
