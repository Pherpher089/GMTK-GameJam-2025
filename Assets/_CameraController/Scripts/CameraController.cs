using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject m_Target;
    // Start is called before the first frame update
    void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target != null)
        {
            // === POSITION FOLLOWING ===
            Vector3 targetPosition = m_Target.transform.position;
            // targetPosition.y = transform.position.y; // Keep the camera at the same height
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 3f);

            // === ROTATION FOLLOWING ===
            // Get the target's forward direction projected onto the horizontal plane
            Vector3 targetForward = m_Target.transform.forward;
            targetForward.y = 0f;

            if (targetForward.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetForward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            }
        }
    }
}
