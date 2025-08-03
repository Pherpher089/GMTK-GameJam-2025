using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStateManager : MonoBehaviour
{
    public static WinStateManager Instance { get; private set; }
    GameObject m_WinStateUI;
    public Image[] m_Stars;
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
        m_WinStateUI = transform.GetChild(3).gameObject;
    }

    public void ShowWinState(int stars)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (m_WinStateUI != null)
        {
            m_WinStateUI.SetActive(true);
        }

        int starCount = TarotController.Instance.m_TarotCardCount;

        for (int i = 0; i < m_Stars.Length; i++)
        {
            if (i == 0)
            {
                m_Stars[i].color = Color.white;
            }
            else if (i == 1)
            {
                if (starCount > 0)
                {
                    m_Stars[i].color = Color.white;
                }
                else
                {
                    m_Stars[i].color = Color.gray;
                }
            }
            else if (i == 2)
            {
                if (starCount == TarotController.Instance.m_TotalTarotCards)
                {
                    m_Stars[i].color = Color.white;
                }
                else
                {
                    m_Stars[i].color = Color.gray;
                }
            }
        }

    }
}
