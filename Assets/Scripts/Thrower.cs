using UnityEngine;

public class Thrower: Singleton<Thrower>
{
    [SerializeField] private float tossFruitForce = 100f;
    [SerializeField] private float tossFruitTorque = 1;

    [SerializeField, Tooltip("The percentage of the screen where can objects be thrown")] 
    private float screenPercentageForThrownX = 1;


    [SerializeField] 
    private Bounds bounds = new Bounds(
        new Vector3(0f, 4f, 0f), 
        new Vector3(4f, 0f, 0f)
        );


    public void Awake()
    {
        InitializeTossingField();
    }

    public void InitializeTossingField() 
    { 
        var camera = Camera.main;

        // The order of the corners is lower left, upper left, upper right, lower right.
        var corners = new Vector3[4];
        camera.CalculateFrustumCorners(camera.rect, -camera.transform.position.z, 
                                            camera.stereoActiveEye, corners);

        var fieldMin = new Vector3(corners[0].x * screenPercentageForThrownX, corners[0].y);
        var fieldMax = new Vector3(corners[2].x * screenPercentageForThrownX, corners[2].y);

        var fieldCenter = new Vector3(camera.transform.position.x, camera.transform.position.y, 0f);
        var fieldSize = (fieldMax - fieldMin);
        bounds = new Bounds(fieldCenter, fieldSize);
    }

    public void Throw(Rigidbody rigidbody)
    {
        var torqueDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        var x = Random.Range(bounds.min.x, bounds.max.x);

        var startPos = new Vector3(x, 0f, 0f);
        var directionToCenterOfBounds = (Vector3.up + (bounds.center - startPos).normalized * Random.Range(0f, 0.5f)).normalized;

        var force = directionToCenterOfBounds * tossFruitForce;
        var torque = torqueDirection * tossFruitTorque;

        rigidbody.transform.position = startPos;
        rigidbody.AddForce(force);
        rigidbody.AddTorque(torque);
    }

}
