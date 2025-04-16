using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Windows;
using EnhancedTouch =  UnityEngine.InputSystem.EnhancedTouch;

public delegate void InputManagerTouchEvent(Vector2 position, Vector2 delta, int fingerId);

public class InputManager : MonoBehaviour
{
    // Initialize with empty delegate to prevent null reference
    public InputManagerTouchEvent OnTouchStart = delegate { };
    public InputManagerTouchEvent OnTouchMove = delegate { };
    public InputManagerTouchEvent OnTouchEnd = delegate { };


    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void Update()
    {
        foreach (EnhancedTouch.Touch touch in EnhancedTouch.Touch.activeTouches)
        {
            if(touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                OnTouchStart?.Invoke(touch.startScreenPosition, touch.delta, touch.finger.index);
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                OnTouchMove?.Invoke(touch.screenPosition, touch.delta, touch.finger.index);
            } else if(touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                OnTouchEnd?.Invoke(touch.screenPosition, touch.delta, touch.finger.index);
            }
        }
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
