using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float followSpeed = 8f;
    GameObject m_Target;
    ThirdPersonWitchController m_TargetController;
    // Start is called before the first frame update
    void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player");
        m_TargetController = m_Target.GetComponent<ThirdPersonWitchController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (m_Target != null)
        {
            // === POSITION FOLLOWING ===
            Vector3 targetPosition = m_Target.transform.position;
            // targetPosition.y = transform.position.y; // Keep the camera at the same height
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

            // === ROTATION FOLLOWING ===
            // Get the target's forward direction projected onto the horizontal plane
            Vector3 targetForward = m_Target.transform.forward;
            targetForward.y = 0f;

            if (targetForward.sqrMagnitude > 0.001f && m_TargetController != null && m_TargetController.m_IsGrounded)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetForward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            }
        }
    }
}
