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
    private float currentSpeed = 0f;
    private float alignSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public LayerMask groundLayer = 6;
    public float groundCheckDistance = 0.1f;

    private Rigidbody m_Rigidbody;
    private bool m_IsGrounded;
    private Vector3 m_MoveDirection;
    private Vector3 m_GroundNormal = Vector3.up;
    private GameObject m_WitchObject;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGrounded();
    }

    public void Move(float horizontal, float acceleration, float brake, bool jump)
    {
        Debug.Log("### acceleration: " + acceleration + ", brake: " + brake + ", horizontal: " + horizontal + ", grounded: " + m_IsGrounded);
        if (acceleration > .1)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += accelerationSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = maxSpeed;
            }
        }

        if (brake > .1)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brake * Time.deltaTime * brakingDeceleration;
            }
            else
            {
                currentSpeed = 0;
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
            m_MoveDirection = Vector3.Lerp(m_MoveDirection, forward * maxSpeed, currentSpeed * Time.deltaTime);
        }
        else
        {
            m_MoveDirection = Vector3.Lerp(m_MoveDirection, Vector3.zero, coastingDeceleration * Time.deltaTime);
        }

        // Apply movement
        Vector3 targetVelocity = new Vector3(m_MoveDirection.x, m_Rigidbody.velocity.y, m_MoveDirection.z);
        m_Rigidbody.velocity = targetVelocity;


        float turn = horizontal * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);
        if (!m_IsGrounded)
        {
            Quaternion uprightRotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, uprightRotation, Time.deltaTime * alignSpeed);
        }


        // Jumping
        if (jump && m_IsGrounded)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, jumpForce, m_Rigidbody.velocity.z);
        }
    }

    private void CheckGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance + 0.1f, groundLayer))
        {
            m_IsGrounded = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize ground check
        Gizmos.color = m_IsGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.1f));
    }
}
