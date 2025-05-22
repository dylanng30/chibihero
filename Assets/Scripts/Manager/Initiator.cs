using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Initiator : MonoBehaviour
{
    private void Start()
    {
       // StartCoroutine(InitGame());
    }
    /*private IEnumerator InitGame()
    {
        List<IInitialize> systems = new List<IInitialize>
        {
            //System core
            Systems.Instance,
            GameManagerTest.Instance,

        };

        foreach (var system in systems)
        {
            Debug.Log($"Initializing {system.GetType().Name}...");
            yield return StartCoroutine(system.Init());
        }
        Debug.Log("All systems are initialized");
    }*/
}
