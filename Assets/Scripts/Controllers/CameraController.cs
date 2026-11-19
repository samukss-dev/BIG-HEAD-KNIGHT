using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

[RequireComponent(typeof(CinemachineOrbitalFollow))]
public class CameraController : MonoBehaviour
{
    [SerializeField] float sensibilidadeToque = 0.15f;
    [SerializeField] float sensibilidadeMouse = 0.1f;

    CinemachineOrbitalFollow orbital;
    int dedoId = -1;
    bool arrastandoMouse;

    void Awake()
    {
        orbital = GetComponent<CinemachineOrbitalFollow>();

        var inputController = GetComponent<CinemachineInputAxisController>();
        if (inputController != null) inputController.enabled = false;
    }

    void OnEnable() => EnhancedTouchSupport.Enable();
    void OnDisable() => EnhancedTouchSupport.Disable();

    void Update()
    {
        LerToque();
        LerMouse();
    }

    void LerToque()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (dedoId == -1 &&
                    !EventSystem.current.IsPointerOverGameObject(touch.touchId))
                    dedoId = touch.touchId;
            }
            else if (touch.touchId == dedoId)
            {
                if (touch.phase == TouchPhase.Moved)
                    Girar(touch.delta, sensibilidadeToque);
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    dedoId = -1;
            }
        }
    }

    void LerMouse()
    {
        var mouse = Mouse.current;
        if (mouse == null) return; // no celular não tem mouse

        if (mouse.leftButton.wasPressedThisFrame)
            arrastandoMouse = !EventSystem.current.IsPointerOverGameObject();

        if (mouse.leftButton.wasReleasedThisFrame)
            arrastandoMouse = false;

        if (arrastandoMouse)
            Girar(mouse.delta.ReadValue(), sensibilidadeMouse);
    }

    void Girar(Vector2 delta, float sensibilidade)
    {
        orbital.HorizontalAxis.Value += delta.x * sensibilidade;

        var faixa = orbital.VerticalAxis.Range;
        orbital.VerticalAxis.Value = Mathf.Clamp(
            orbital.VerticalAxis.Value - delta.y * sensibilidade,
            faixa.x, faixa.y);
    }
}