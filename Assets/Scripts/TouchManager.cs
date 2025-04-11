using System;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;


public delegate void TouchManagerEvent();

public class TouchManager : MonoBehaviour
{
    public TouchManagerEvent OnBombTouch;

    [SerializeField] private List<GameObject> _fingers;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _sliceForce = 100f;
    [SerializeField] private float _sliceTorque = 100f;
    [SerializeField] private int _maxCountOfFingers = 10;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private LayerMask fruitMask;
    [SerializeField] private LayerMask bombMask;

    private RaycastHit hit;
    private Vector3 lastMousePosition;

    Camera Camera => Camera.main;


    private void OnEnable()
    {
        soundManager = (SoundManager)SoundManager.Instance;

        _inputManager = GameObject
            .FindGameObjectWithTag("InputManager")
            .GetComponent<InputManager>();
        _inputManager.OnTouchStart += StartTouch;
        _inputManager.OnTouchMove += MoveTouch;
        _inputManager.OnTouchEnd += EndTouch;

        InitializeFingers();
    }

    public void InitializeFingers()
    {
        _fingers = new(_maxCountOfFingers);

        for (int i = 0; i < _maxCountOfFingers; i++) 
        {
            var fingerObject = Instantiate(_trailRenderer.gameObject, this.transform);
            fingerObject.name = $"Finger: {i}";
            _fingers.Add(fingerObject);
        }
    }

    private void MoveTouch(Vector2 position, Vector2 delta, int fingerId)
    {
        if (fingerId >= _fingers.Count) return;
        var finger = _fingers[fingerId];
        finger.SetActive(true);


        Vector3 clickPosition = position;
        clickPosition.z = -Camera.transform.position.z;

        Ray ray = Camera.ScreenPointToRay(clickPosition);
        var mouseWorldPosition = Camera.ScreenToWorldPoint(clickPosition);

        var direction = (delta).normalized;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, fruitMask | bombMask))
        {
            var hitObject = hit.rigidbody.gameObject;
            CollisionEvent(hitObject, direction);
        };

        var pos = ray.origin;
        pos.z = 0;
        mouseWorldPosition.z = 0f;

        finger.transform.position = mouseWorldPosition;
        lastMousePosition = pos;
        soundManager.PlayWhooshSound();
    }

    private void StartTouch(Vector2 position, Vector2 delta, int fingerId)
    {
        if (fingerId >= _fingers.Count) return;
        var finger = _fingers[fingerId];

        Vector3 clickPosition = position;
        clickPosition.z = -Camera.transform.position.z;

        var mouseWorldPosition = Camera.ScreenToWorldPoint(clickPosition);
        finger.transform.position = mouseWorldPosition;
    }

    private void EndTouch(Vector2 position, Vector2 delta, int fingerId)
    {
        if (fingerId >= _fingers.Count) return;
        var finger = _fingers[fingerId];
        finger.SetActive(false);
    }

    public void CollisionEvent(GameObject gameObject, Vector3 sliceDirection)
    {
        if((fruitMask & 1 << gameObject.layer) != 0)
        {
            SliceFruitEvent(gameObject, sliceDirection);
        }
        else if((bombMask & 1 << gameObject.layer) != 0)
        {
            SliceBombEvent(gameObject, sliceDirection);
        }
    }

    public void SliceFruitEvent(GameObject gameObject, Vector3 sliceDirection)
    {
        var fruit = gameObject.GetComponentInParent<Fruit>();
        fruit.SliceFruit(sliceDirection, _sliceForce, _sliceTorque);
    }

    public void SliceBombEvent(GameObject gameObject, Vector3 sliceDirection)
    {
        var bomb = gameObject.GetComponentInParent<Bomb>();
        bomb.TriggerBomb();
        OnBombTouch?.Invoke();
    }

    private void DeleteFingers()
    {
        foreach (var finger in _fingers) 
        { 
            if (finger != null)
                Destroy(finger);    
        }
        _fingers.Clear();   
    }

    private void OnDisable()
    {
        DeleteFingers();

        _inputManager.OnTouchStart -= StartTouch;
        _inputManager.OnTouchStart -= MoveTouch;
    }
}
