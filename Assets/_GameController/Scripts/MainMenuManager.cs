using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public string m_GameSceneName;

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(m_GameSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OnCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
    public void OnMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
