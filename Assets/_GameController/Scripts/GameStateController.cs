using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    GameOver
}
public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }
    GameObject m_TimerObject;
    GameObject m_LooseScreenObject;

    public GameState m_CurrentState = GameState.Playing;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        m_TimerObject = transform.GetChild(0).gameObject;
        m_LooseScreenObject = transform.GetChild(1).gameObject;
    }

    public void LooseGame()
    {
        m_TimerObject.SetActive(false);
        m_LooseScreenObject.SetActive(true);
        m_CurrentState = GameState.GameOver;
        Debug.Log("Game Over! You lost.");
    }

    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
