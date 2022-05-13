using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    private Camera cam;
    public float MovementSpeed = 10;
    public float Gravity = 0.0f;
    private float velocity = 0;

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
    }
}