using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {        
        //Debug.Log("Play button clicked");
        GameManager.Instance.ChangeState(GameState.Exploring);
    }
}
