using UnityEngine;

public delegate void FruitEvent();

public class Fruit : MonoBehaviour, ICacheObject
{
    public FruitEvent OnWholeFruitFall;
    public FruitEvent OnFruitSlice;

    [SerializeField] private FruitParticleManager _particleManager;
    [SerializeField] private WholeFruit _wholeFruit;
    [SerializeField] private SlicedFruit _slicedFruit;

    [SerializeField] private bool isSliced = false;

    public WholeFruit WholeFruit => _wholeFruit;
    public SlicedFruit SlicedFruit => _slicedFruit;

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
        _slicedFruit.Slice(_wholeFruit.transform.position, direction, sliceForce, sliceTorque);
        isSliced = true;
        OnFruitSlice.Invoke();
    }

    public void CheckHeight()
    {
        if (!isSliced)
        {
            if (_wholeFruit.transform.position.y < -1f)
            {
                OnWholeFruitFall?.Invoke();
                Deactivate();
            }
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
        _wholeFruit.Activate();
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

        OnWholeFruitFall = null;
        OnFruitSlice = null;
    }

    public void TossFruit(Vector3 startPosition, Vector3 force, Vector3 torque)
    {
        Reset();
        _wholeFruit.Activate(startPosition, force, torque);

    }

    public void Activate(string name, Vector3 position)
    {
        gameObject.name = name;
        gameObject.transform.position = position;

        Activate();
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
