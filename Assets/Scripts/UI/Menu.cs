using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject tutorPanel;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject tutorButton;
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject unmuteButton;
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
    public void Mute()
    {
        AudioListener.volume = 0f;
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
    }
    public void Unmute()
    {
        AudioListener.volume = 1f;
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
    }
}
