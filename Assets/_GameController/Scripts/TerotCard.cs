using UnityEngine;

public class TerotCard : MonoBehaviour
{
    bool m_IsCollected = false;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !m_IsCollected)
        {
            m_IsCollected = true;
            TerotController.Instance.AddTerotCard();
            Destroy(gameObject);
        }
    }
}
