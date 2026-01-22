using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoopUI : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clickSounds = new List<AudioClip>();

    public void OpenStartScene()
    {
        PlayerClickSound();
        SceneManager.LoadScene("StartScene");
    }

    public void OpenDeckScene()
    {
        PlayerClickSound();
        SceneManager.LoadScene("DeckScene");
    }

    public void OpenFightScene()
    {
        PlayerClickSound();
        SceneManager.LoadScene("MainScene");
    }
    public void OpenCredits()
    {
        PlayerClickSound();
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        PlayerClickSound();
        Application.Quit();
        Debug.Log("Quit");
    }
    public void PlayerClickSound()
    {
        AudioManager.Instance.PlayClip(clickSounds);
    }
}
