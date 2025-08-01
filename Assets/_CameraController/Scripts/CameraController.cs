using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance { get; set; }
    public float followSpeed = 8f;
    public GameObject m_Target;
    ThirdPersonWitchController m_TargetController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player");
        if (m_Target != null)
        {
            m_TargetController = m_Target.GetComponent<ThirdPersonWitchController>();
        }
    }

    public void SetTarget(GameObject target)
    {
        m_Target = target;
        if (m_Target != null)
        {
            m_TargetController = m_Target.GetComponent<ThirdPersonWitchController>();
        }
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
