using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoopUI : MonoBehaviour
{
    public void OpenStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OpenDeckScene()
    {
        SceneManager.LoadScene("DeckScene");
    }

    public void OpenFirstFightScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
