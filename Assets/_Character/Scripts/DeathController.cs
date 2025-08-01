using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{

    public GameObject m_LineSpawner;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Destroy(other.gameObject);
            m_LineSpawner.transform.parent = null; // Detach the line spawner from its parent
            Destroy(gameObject); // Destroy the player character
        }
        if (other.CompareTag("Fall"))
        {
            m_LineSpawner.transform.parent = null; // Detach the line spawner from its parent
            Destroy(gameObject); // Destroy the player character
        }
    }
}
