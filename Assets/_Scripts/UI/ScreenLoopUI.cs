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
    public void OpenSecondFightScene()
    {
        SceneManager.LoadScene("MainScene2");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
