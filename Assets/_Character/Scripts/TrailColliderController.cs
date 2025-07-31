using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColliderController : MonoBehaviour
{
    public float boostForce = 1;
    public void OnTriggerEnter(Collider other)
    {
        // Handle collision with the trail collider
        if (other.CompareTag("Player"))
        {
            ThirdPersonWitchController witchController = other.GetComponent<ThirdPersonWitchController>();
            if (witchController != null)
            {
                witchController.m_ActiveBoosts.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Handle exit from the trail collider
        if (other.CompareTag("Player"))
        {
            ThirdPersonWitchController witchController = other.GetComponent<ThirdPersonWitchController>();
            if (witchController != null)
            {
                witchController.m_ActiveBoosts.Remove(gameObject);
            }
        }
    }
}
