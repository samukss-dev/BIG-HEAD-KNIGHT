using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Input Settings")]
    private PlayerInput playerInput;
    private Vector2 moveInput;
    [Header("Player Settings")]
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime * speed);
    }
}
