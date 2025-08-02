using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerotController : MonoBehaviour
{
    public static TerotController Instance { get; set; }
    public int m_TerotCardCount = 0; // Count of Terot cards collected
    public TextMeshProUGUI m_TerotCardText; // UI Text to display the count

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
        m_TerotCardText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateTerotCardText();
    }

    public void AddTerotCard()
    {
        m_TerotCardCount++;
        UpdateTerotCardText();
    }

    void UpdateTerotCardText()
    {
        if (m_TerotCardText != null)
        {
            m_TerotCardText.text = m_TerotCardCount.ToString();
        }
    }
}
