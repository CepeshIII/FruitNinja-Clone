using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using EnhancedTouch =  UnityEngine.InputSystem.EnhancedTouch;

public delegate void InputManagerEvent();

public class InputManager : MonoBehaviour
{
    private Vector2 mousePosition = Vector2.zero;
    private InputSystem_Actions _Input = null;
    
    // Initialize with empty delegate to prevent null reference
    public InputManagerEvent OnClick = delegate { };

    public Vector2 MousePosition => mousePosition;

    private void OnEnable()
    {
        _Input = new();
        _Input.Player.Enable();

        //_Input.Player.Click.performed += HandleClick;
        //_Input.Player.MousePositon.performed += HandleMousePosition;
        //_Input.Player.TouchPress.performed += HandleClick;
        _Input.Player.TouchPosition.performed += HandleMousePosition;
    }

    private void HandleClick(InputAction.CallbackContext context)
    {
        OnClick?.Invoke(); // Safe invocation
    }

    private void HandleMousePosition(InputAction.CallbackContext context)
    {
        Vector2 position = context.ReadValue<Vector2>();
        mousePosition = position;
    }

    private void OnDisable()
    {
        //_Input.Player.Click.started -= HandleClick;
        //_Input.Player.MousePositon.started -= HandleMousePosition;
        _Input.Player.TouchPosition.performed -= HandleMousePosition;
        //_Input.Player.TouchPress.performed -= HandleClick;

        _Input.Player.Disable();

    }

    public void Update()
    {
        //foreach(EnhancedTouch.Touch touch in EnhancedTouch.Touch.activeTouches)
        //{
        //    if(touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
        //    {
        //        OnClick.Invoke();
        //    }
        //}

        //if (_Input.Player.TouchPress.WasPerformedThisFrame()) 
        //{
            if (_Input.Player.TouchPress.IsPressed())
            {
                OnClick.Invoke();
            }
        //}

    }

}
