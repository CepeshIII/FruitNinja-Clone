using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FruitManager _fruitManager;
    [SerializeField] private TouchManager _touchManager;
    [SerializeField] private BombManager _bombManager;

    private void OnEnable()
    {
        _fruitManager = GameObject.FindGameObjectWithTag("FruitManager").GetComponent<FruitManager>();
        _touchManager = GameObject.FindGameObjectWithTag("TouchManager").GetComponent<TouchManager>();
        _bombManager = GameObject.FindGameObjectWithTag("BombManager").GetComponent<BombManager>();

        _touchManager.OnBombTouch += BombTouch;
        _fruitManager.OnMissFruit += MissFruit;
    }

    public void MissFruit()
    {
        Debug.Log("Game over: Miss Fruit");
    }

    public void BombTouch()
    {
        Debug.Log("Game over: BombTouch");
    }

    private void OnDisable()
    {
        _touchManager.OnBombTouch -= BombTouch;
        _fruitManager.OnMissFruit -= MissFruit;
    }
}
