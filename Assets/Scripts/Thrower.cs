using UnityEngine;

public class Thrower: Singleton<Thrower>
{
    [SerializeField] private float tossFruitForce = 100f;
    [SerializeField] private float tossFruitTorque = 1;

    [SerializeField] 
    private Bounds bounds = new Bounds(
        new Vector3(0f, 4f, 0f), 
        new Vector3(4f, 0f, 0f)
        );


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
