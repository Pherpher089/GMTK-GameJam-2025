using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerRespawnController : MonoBehaviour
{

    public GameObject m_Character;
    private GameObject m_CharacterInstance;
    // Start is called b
    void Start()
    {
        m_CharacterInstance = null;
        GetComponent<MeshRenderer>().enabled = false; // Hide the respawn point
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CharacterInstance == null && GameStateController.Instance.m_CurrentState == GameState.Playing)
        {
            m_CharacterInstance = Instantiate(m_Character, transform.position, transform.rotation);
            CameraController.Instance.SetTarget(m_CharacterInstance);
        }
    }
}
