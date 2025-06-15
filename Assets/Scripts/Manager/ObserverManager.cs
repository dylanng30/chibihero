using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : Singleton<ObserverManager>
{
    [SerializeField] private List<IObserver> observers = new();
    protected override void Awake()
    {
        base.Awake();
    }

    public void RegisterObserver(IObserver observer)
    {
        if(observers.Contains(observer)) return;
        observers.Add(observer);
        Debug.Log("Add Observer:" + observer.ToString());
    }
    public void UnregisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer)) return;
        observers.Remove(observer);
        Debug.Log("Remove Observer:" + observer.ToString());
    }

    public void KingIsDead()
    {
        foreach (IObserver observer in observers)
        {
            observer.KingIsDead();
        }
    }
    public void ChangeMap(GameState state)
    {
        foreach (IObserver observer in observers)
        {
            observer.ChangeMap(state);
        }
    }


}
