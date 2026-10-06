using Unity.Cinemachine;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private CinemachineInputAxisController freeLook;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        freeLook = GetComponent<CinemachineInputAxisController>();
        freeLook.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool canMove = Input.GetMouseButton(1); 
        freeLook.enabled = canMove;
        if (canMove)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
