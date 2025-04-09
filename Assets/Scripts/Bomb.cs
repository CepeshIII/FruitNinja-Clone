using UnityEngine;

public class Bomb : MonoBehaviour, ICacheObject
{
    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckHeight();
    }

    public void CheckHeight()
    {
        if (transform.position.y < -1f) Deactivate();
    }

    public void Activate(string name, Vector3 position)
    {
        gameObject.name = name;
        transform.position = position;
        gameObject.SetActive(true);
        Reset();
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
        Reset();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
