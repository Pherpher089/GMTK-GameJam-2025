using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonWitchController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 10f;
    public float acceleration = 15f;
    public float deceleration = 0.25f;
    public float turnSpeed = 100f;
    private float currentSpeed = 0f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public LayerMask groundLayer = 1;
    public float groundCheckDistance = 0.1f;

    private Rigidbody m_Rigidbody;
    private bool isGrounded;
    private Vector3 moveDirection;

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
        if (acceleration > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (brake > 0)
        {
            currentSpeed -= brake * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Forward/backward movement
        Vector3 forward = transform.forward * currentSpeed;

        // Apply currentSpeed or deceleration
        if (currentSpeed != 0)
        {
            moveDirection = Vector3.Lerp(moveDirection, forward * maxSpeed, currentSpeed * Time.deltaTime);
        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Apply movement
        Vector3 targetVelocity = new Vector3(moveDirection.x, m_Rigidbody.velocity.y, moveDirection.z);
        m_Rigidbody.velocity = targetVelocity;


        float turn = horizontal * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);


        // Jumping
        if (jump && isGrounded)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, jumpForce, m_Rigidbody.velocity.z);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize ground check
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.1f));
    }
}
