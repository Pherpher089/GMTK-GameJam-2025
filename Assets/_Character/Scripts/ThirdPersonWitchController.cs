using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonWitchController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 5f;
    public float accelerationSpeed = 1f;
    public float coastingDeceleration = 4f;
    public float brakingDeceleration = 10f;
    public float turnSpeed = 100f;
    public float airTurnSpeed = 300f;
    public float currentSpeed = 0f;
    private float alignSpeed = 10f;
    public bool m_IsBoosting = false;
    public List<GameObject> m_ActiveBoosts = new List<GameObject>();
    public float m_boostModifier = 2f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public LayerMask groundLayer = 6;
    public float groundCheckDistance = 0.1f;

    private Rigidbody m_Rigidbody;
    public bool m_IsGrounded;
    private Vector3 m_MoveDirection;
    private Vector3 m_GroundNormal = Vector3.up;
    private GameObject m_WitchObject;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_WitchObject = transform.GetChild(0).gameObject; // Assuming the witch model is the first child
    }

    void FixedUpdate()
    {
        CheckGrounded();
        if (m_ActiveBoosts.Count > 0 && !m_IsBoosting)
        {
            m_IsBoosting = true;
            maxSpeed *= m_boostModifier;
        }
        else if (m_ActiveBoosts.Count == 0 && m_IsBoosting)
        {
            m_IsBoosting = false;
            maxSpeed /= m_boostModifier;
        }
    }

    public void Move(float horizontal, float acceleration, float brake, bool jump)
    {
        if (GameStateController.Instance.m_CurrentState != GameState.Playing) return;
        if (m_IsGrounded)
        {
            if (acceleration > .1)
            {
                if (currentSpeed < maxSpeed)
                {
                    currentSpeed += accelerationSpeed * Time.deltaTime;
                    if (m_IsBoosting)
                    {
                        currentSpeed *= m_boostModifier;
                    }
                }
                else
                {
                    currentSpeed = maxSpeed;
                }

                if (m_IsBoosting)
                {

                }
            }

            if (brake > .1)
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= brake * Time.deltaTime * brakingDeceleration;
                }
            }
        }

        if (brake <= 0.1 && acceleration <= 0.1)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, coastingDeceleration * Time.deltaTime);
        }

        // Forward/backward movement
        Vector3 forward = transform.forward * currentSpeed;

        // Apply currentSpeed or deceleration
        if (currentSpeed != 0)
        {
            m_MoveDirection = Vector3.Lerp(m_MoveDirection, forward, currentSpeed * Time.deltaTime);
        }
        else
        {
            m_MoveDirection = Vector3.Lerp(m_MoveDirection, Vector3.zero, coastingDeceleration * Time.deltaTime);
        }

        // Apply movement
        if (m_IsGrounded)
        {
            Vector3 targetVelocity = new Vector3(m_MoveDirection.x, m_Rigidbody.velocity.y, m_MoveDirection.z);
            m_Rigidbody.velocity = targetVelocity;
        }


        float turn = 0f;
        if (m_IsGrounded)
        {
            turn = horizontal * turnSpeed * Time.deltaTime;

        }
        else
        {
            turn = horizontal * airTurnSpeed * Time.deltaTime;

        }

        // Add Rotation
        transform.Rotate(0, turn, 0);
        if (m_IsGrounded)
        {
            //lean left or right based on horizontal input
            if (horizontal != 0 && currentSpeed > 0.5f)
            {
                Quaternion targetLean = Quaternion.Euler(horizontal * 30f, 90, 0); // Z-axis for leaning
                m_WitchObject.transform.localRotation = Quaternion.Slerp(
                    m_WitchObject.transform.localRotation,
                    targetLean,
                    Time.deltaTime * 5f // adjust for snappier or slower lean
                );
            }
            else
            {
                // Return to upright smoothly when stopped
                m_WitchObject.transform.localRotation = Quaternion.Slerp(
                    m_WitchObject.transform.localRotation,
                    Quaternion.Euler(0, 90, 0),
                    Time.deltaTime * 5f
                );
            }

        }

        // Align to ground normal
        Quaternion uprightRotation = Quaternion.FromToRotation(transform.up, m_GroundNormal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, uprightRotation, Time.deltaTime * alignSpeed);

        // Jumping
        if (jump && m_IsGrounded)
        {
            m_Rigidbody.velocity += m_Rigidbody.transform.up * jumpForce;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, groundCheckDistance + 0.1f, groundLayer))
        {
            m_IsGrounded = true;
            m_GroundNormal = hit.normal;
        }
        else
        {
            m_IsGrounded = false;

            Vector3 velocity = m_Rigidbody.velocity;

            if (velocity.sqrMagnitude > 0.01f)
            {
                // Step 1: Normalize velocity
                Vector3 velDir = velocity.normalized;

                // Step 2: Project world up onto the plane perpendicular to velocity
                m_GroundNormal = Vector3.ProjectOnPlane(Vector3.up, velDir).normalized;

                // Fallback: if projection fails (e.g. velocity straight up), reset
                if (m_GroundNormal.sqrMagnitude < 0.01f)
                    m_GroundNormal = Vector3.up;
            }
            else
            {
                m_GroundNormal = Vector3.up;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize ground check
        Gizmos.color = m_IsGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.1f));
    }
}
