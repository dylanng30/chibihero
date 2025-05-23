using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {        
        //Debug.Log("Play button clicked");
        GameManagerTest.Instance.ChangeState(GameState.Exploring);
    }
}
