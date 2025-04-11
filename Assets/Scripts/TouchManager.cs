using UnityEngine;

public delegate void TouchManagerEvent();

public class TouchManager : MonoBehaviour
{
    public TouchManagerEvent OnBombTouch;

    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _sliceForce = 100f;
    [SerializeField] private float _sliceTorque = 100f;

    [SerializeField] private LayerMask fruitMask;
    [SerializeField] private LayerMask bombMask;
    private RaycastHit hit;
    private Vector3 lastMousePosition;

    private void OnEnable()
    {
        _inputManager = GameObject
            .FindGameObjectWithTag("InputManager")
            .GetComponent<InputManager>();
        _inputManager.OnClick += () => Click(_inputManager.MousePosition);
    }


    public void Click(Vector2 position)
    {
        _trailRenderer.enabled = true;

        Camera camera = Camera.main;

        Vector3 clickPosition = position;
        clickPosition.z = -camera.transform.position.z;

        Ray ray = camera.ScreenPointToRay(clickPosition);
        var mouseWorldPosition = camera.ScreenToWorldPoint(clickPosition);

        var direction = (new Vector3(ray.origin.x, ray.origin.y) - lastMousePosition).normalized;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, fruitMask | bombMask))
        {
            var hitObject = hit.rigidbody.gameObject;
            CollisionEvent(hitObject, direction);
        };

        var pos = ray.origin;
        pos.z = 0;
        mouseWorldPosition.z = 0f;

        _trailRenderer.transform.position = mouseWorldPosition;
        lastMousePosition = pos;
        _trailRenderer.enabled = false;
    }


    void Update()
    {

        //if (Input.GetMouseButton(1)) 
        //{
        //    //Click(Input.mousePosition);
        //}
        //else
        //{
        //    //_trailRenderer.enabled = false;
        //}
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

    private void OnDisable()
    {
        _inputManager.OnClick -= () => Click(_inputManager.MousePosition);
    }
}
