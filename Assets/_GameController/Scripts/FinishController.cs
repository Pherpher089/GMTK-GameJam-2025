using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int stars = TarotController.Instance.m_TarotCardCount;
            WinStateManager.Instance.ShowWinState(stars);
        }
    }
}
