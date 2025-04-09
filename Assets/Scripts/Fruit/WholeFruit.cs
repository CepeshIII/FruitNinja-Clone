using UnityEngine;

public class WholeFruit : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Activate(Vector3 position, Vector3 force, Vector3 torque)
    {
        gameObject.SetActive(true);

        _rigidbody.transform.position = position;
        AddForce(force, torque);
    }

    public void Reset()
    {
        if (_rigidbody == null) return;
        _rigidbody.transform.rotation = Quaternion.identity;
        _rigidbody.transform.position = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.ResetInertiaTensor();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public bool CheckIfLowerThanHeight(float y)
    {
        if (_rigidbody.transform.position.y < y) return true;
        return false;
    }


    public void AddForce(Vector3 force, Vector3 torque)
    {
        _rigidbody.AddForce(force);
        _rigidbody.AddTorque(torque);
    }

}
