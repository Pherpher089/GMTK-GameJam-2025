using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    public float roundTime = 10f; // Example round time in seconds
    TextMeshProUGUI timerText;
    void Start()
    {
        timerText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        if (timerText == null)
        {
            Debug.LogError("Timer Text component not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            GameStateController.Instance.LooseGame();
        }
    }

    public void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(roundTime / 60);
            int seconds = Mathf.FloorToInt(roundTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ResetTimer()
    {
        roundTime = 10f; // Reset to initial round time
        UpdateTimerText();
    }
}
