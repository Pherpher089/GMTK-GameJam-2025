using UnityEngine;

public class ThirdPersonUserControl : MonoBehaviour
{
    ThirdPersonWitchController m_Character;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        m_Character = GetComponent<ThirdPersonWitchController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float accelerate = Input.GetAxis("Accelerate");
        float brake = Input.GetAxis("Brake");
        bool jump = Input.GetButtonDown("Jump");

        m_Character.Move(horizontal, accelerate, brake, jump);
    }
}
