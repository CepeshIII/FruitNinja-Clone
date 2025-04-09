using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _sliceForce = 100f;
    [SerializeField] private float _sliceTorque = 100f;

    [SerializeField] private LayerMask fruitMask;
    [SerializeField] private LayerMask bombMask;
    private RaycastHit hit;
    private Vector3 lastMousePosition;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButton(0)) 
        {
            Camera camera = Camera.main;

            var mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -camera.transform.position.z;

            Ray ray = camera.ScreenPointToRay(mouseScreenPos);
            var mouseWorldPosition = camera.ScreenToWorldPoint(mouseScreenPos);

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
        }
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
        Debug.Log("Game over");
    }
}
