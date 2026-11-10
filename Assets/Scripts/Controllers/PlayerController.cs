using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Input Settings")]
    private PlayerInput playerInput;
    private Vector2 moveInput;
    [Header("Player Settings")]
    public float speed = 5f;
    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float rotationSpeed = 360f; // Degrees per second
    private Transform cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }
    void Start()
    {
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camRight = cam.right;
        Vector3 camForward = cam.forward;
        camRight.y = 0;
        camForward.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 movementDirection = moveInput.x * camRight + moveInput.y * camForward;
        if (movementDirection.sqrMagnitude > 0.01f)
        {

            movementDirection.Normalize();
  
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            
        } else
        {
            movementDirection = Vector3.zero;
        }
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMovement = movementDirection * speed + new Vector3(0, velocity.y, 0);
        controller.Move(finalMovement * Time.deltaTime);

    }
}
