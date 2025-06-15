using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject tutorPanel;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject tutorButton;
    public void Play()
    {        
        //Debug.Log("Play button clicked");
        GameManager.Instance.ChangeState(GameState.Exploring);
    }

    public void ShowTutor()
    {
        tutorPanel.SetActive(true);
        playButton.SetActive(false);
        tutorButton.SetActive(false);
    }
    public void HideTutor()
    {
        tutorPanel.SetActive(false);
        playButton.SetActive(true);
        tutorButton.SetActive(true);
    }
}
