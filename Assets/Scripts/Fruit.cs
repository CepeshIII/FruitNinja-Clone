using UnityEngine;


public class Fruit : MonoBehaviour
{
    [SerializeField] private WholeFruit _wholeFruit;
    [SerializeField] private SlicedFruit _slicedFruit;
    [SerializeField] private float _sliceForce = 100f;
    [SerializeField] private bool isSliced = false;

    private void OnEnable()
    {
        Reset();
    }

    void Update()
    {
        if (!isSliced)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SliceFruit();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _wholeFruit.Reset();
            _slicedFruit.Deactivate();
        }

        CheckHeight();
    }

    public void SliceFruit()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);
        _wholeFruit.Deactivate();
        _slicedFruit.Activate(_wholeFruit.transform.position, new Vector2(x, y), _sliceForce);
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
