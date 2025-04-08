using UnityEngine;

public class SlicedFruit: MonoBehaviour
{

    [SerializeField] private Rigidbody _leftSlice;
    [SerializeField] private Rigidbody _rightSlice;

    public void Activate(Vector3 newPosition, Vector3 direction, float force, float torque)
    {
        gameObject.SetActive(true);

        _leftSlice.transform.position = newPosition;
        _rightSlice.transform.position = newPosition;

        Vector3 sliceNormal = Vector3.Cross(direction, Vector3.forward);

        _leftSlice.transform.rotation = Quaternion.FromToRotation(_leftSlice.transform.right, sliceNormal);
        _rightSlice.transform.rotation = Quaternion.FromToRotation(-_rightSlice.transform.right, sliceNormal);


        //_leftSlice.AddExplosionForce(force, _leftSlice.position + Vector3.right, 2f);
        //_rightSlice.AddExplosionForce(force, _rightSlice.position - Vector3.right, 2f);

        _leftSlice.AddForce(-_leftSlice.transform.right * force);
        _rightSlice.AddForce(-_rightSlice.transform.right * force);

        _leftSlice.AddTorque(-_leftSlice.transform.right + -_leftSlice.transform.forward * torque);
        _rightSlice.AddTorque(-_rightSlice.transform.right + -_rightSlice.transform.forward * torque);
    }

    public void Reset()
    {
        _leftSlice.transform.position = Vector3.zero;
        _rightSlice.transform.position = Vector3.zero;

        _leftSlice.transform.rotation = Quaternion.identity;
        _rightSlice.transform.rotation = Quaternion.identity;

        _leftSlice.angularVelocity = Vector3.zero;
        _rightSlice.angularVelocity = Vector3.zero;

        _leftSlice.linearVelocity = Vector3.zero;
        _rightSlice.linearVelocity = Vector3.zero;

        _leftSlice.ResetInertiaTensor();
        _rightSlice.ResetInertiaTensor();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public bool CheckIfLowerThanHeight(float y)
    {
        if(_rightSlice.position.y < y && _leftSlice.position.y < y) return true;

        return false;
    }
}
