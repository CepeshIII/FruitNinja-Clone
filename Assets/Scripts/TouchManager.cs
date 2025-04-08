using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _sliceForce = 100f;
    [SerializeField] private float _sliceTorque = 100f;

    [SerializeField] private LayerMask mask;
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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) 
            {
                var fruit = hit.rigidbody.gameObject.GetComponentInParent<Fruit>();

                fruit.SliceFruit(direction, _sliceForce, _sliceTorque);
            };

            var pos = ray.origin;
            pos.z = 0;
            mouseWorldPosition.z = 0f;

            _trailRenderer.transform.position = mouseWorldPosition;
            lastMousePosition = pos;
        }
    }
}
