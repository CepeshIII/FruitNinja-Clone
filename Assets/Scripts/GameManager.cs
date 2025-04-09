using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FruitManager _fruitManager;

    private void OnEnable()
    {
        _fruitManager = GameObject.FindGameObjectWithTag("FruitManager").GetComponent<FruitManager>();
        _fruitManager.OnMissFruit += GameOver;
    }

    public void GameOver()
    {
        Debug.Log("Game over: Miss Fruit");
    }
}
