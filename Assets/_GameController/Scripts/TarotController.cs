using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TarotController : MonoBehaviour
{
    public static TarotController Instance { get; set; }
    public int m_TarotCardCount = 0; // Count of Tarot cards collected
    public int m_TotalTarotCards; // Total Tarot cards in the game
    public TextMeshProUGUI m_TarotCardText; // UI Text to display the count

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
        GameObject[] allTarotCards = GameObject.FindGameObjectsWithTag("Tarot");
        m_TotalTarotCards = allTarotCards.Length;
        m_TarotCardText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateTarotCardText();
    }

    public void AddTarotCard()
    {
        m_TarotCardCount++;
        UpdateTarotCardText();
    }

    void UpdateTarotCardText()
    {
        if (m_TarotCardText != null)
        {
            m_TarotCardText.text = m_TarotCardCount.ToString() + " / " + m_TotalTarotCards.ToString();
        }
    }
}
