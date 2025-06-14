using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : Singleton<MineManager>, IObserver
{
    [SerializeField] private List<MineController> mineControllers = new ();

    private void Start()
    {
        ObserverManager.Instance.RegisterObserver(this);     
    }

    public void MineSpawn()
    {
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        if (this.transform.childCount != 0)
            yield break;

        yield return new WaitUntil(() => Systems.Instance.ResourceSystem.Mines != null);
        //Debug.Log(Systems.Instance.ResourceSystem.Mines.Count);
        List<MineSO> mineSOs = Systems.Instance.ResourceSystem.Mines;
        foreach (var mineSO in mineSOs)
        {
            GameObject mine = Instantiate(mineSO.Prefab, mineSO.Position, Quaternion.identity);
            mine.transform.parent = this.transform;
            MineController mineController = mine.GetComponent<MineController>();
            mineController.IsRealMap(mineSO.MineType);
            RegisterMine(mineController);
        }
    }

    private void RegisterMine(MineController mine)
    {
        if (mineControllers.Contains(mine)) return;
        mineControllers.Add(mine);
    }
    public void UnregisterMine(MineController mine)
    {
        if (!mineControllers.Contains(mine)) return;
        mineControllers.Remove(mine);
    }


    private void DeactivateMines()
    {
        foreach (MineController mine in mineControllers)
        {
            mine.SetVisible(false);
        }
    }
    private void ActivateMines()
    {
        foreach (MineController mine in mineControllers)
        {
            mine.SetVisible(true);
        }
    }
    

    public void KingIsDead()
    {
        foreach (MineController mine in mineControllers)
        {
            mine.KingIsDead();
        }
    }
    public void ChangeMap(GameState state)
    {
        if (state == GameState.Exploring)
            ActivateMines();
        else
            DeactivateMines();
    } 


}
