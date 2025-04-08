using UnityEngine;


public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitParticleManager _particleManager;
    [SerializeField] private WholeFruit _wholeFruit;
    [SerializeField] private SlicedFruit _slicedFruit;

    [SerializeField] private bool isSliced = false;

    private void OnEnable()
    {
        Reset();
        _particleManager = GetComponent<FruitParticleManager>();
    }

    void Update()
    {
        CheckHeight();
    }

    public void SliceFruit(Vector3 direction, float sliceForce, float sliceTorque)
    {
        if (isSliced) return;

        _particleManager.CreateParticles(_wholeFruit.transform.position);
        _wholeFruit.Deactivate();
        _slicedFruit.Activate(_wholeFruit.transform.position, direction, sliceForce, sliceTorque);
        isSliced = true;
    }

    public void CheckHeight()
    {
        if (!isSliced)
        {
            if (_wholeFruit.transform.position.y < -1f) Deactivate();
        }
        else 
        { 
            if (_slicedFruit.CheckIfLowerThanHeight(-1f)) Deactivate();
        }
    }

    public void Activate()
    {
        Reset();

        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        Reset();
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        isSliced = false;

        _slicedFruit.Reset();
        _wholeFruit.Reset();

        _slicedFruit.Deactivate();
        _wholeFruit.Deactivate();
    }

    public void TossFruit(Vector3 startPosition, Vector3 force, Vector3 torque)
    {
        Reset();
        _wholeFruit.Activate(startPosition, force, torque);

    }
}
