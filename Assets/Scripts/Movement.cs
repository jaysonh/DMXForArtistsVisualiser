using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    private Camera cam;
    public float MovementSpeed = 10;
    public float Gravity = 0.0f;
    private float velocity = 0;
    private const float MIN_X = -20.0f;
    private const float MAX_X =  7.0f;
    private const float MIN_Y =  2.5f;
    private const float MAX_Y =  10.0f;
    private const float MIN_Z =  0.0f;
    private const float MAX_Z =  20.5f;

    private void Start()
    {
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        // characterController.Move((Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime);
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);

        // Gravity
        if (characterController.isGrounded)
        {
            velocity = 0;
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, velocity, 0));
        }

        // Clamp movement to inside building
        transform.position = new Vector3(
              Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
              Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
              Mathf.Clamp(transform.position.z, MIN_Z, MAX_Z));
    }
}