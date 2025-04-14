using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FruitManager _fruitManager;
    [SerializeField] private TouchManager _touchManager;
    [SerializeField] private BombManager _bombManager;
    [SerializeField] private CacheObjectHolder _cacheObjectHolder;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private ScoreManager _scoreManager;

    private void OnEnable()
    {
        _fruitManager = GameObject.FindGameObjectWithTag("FruitManager").GetComponent<FruitManager>();
        _touchManager = GameObject.FindGameObjectWithTag("TouchManager").GetComponent<TouchManager>();
        _bombManager = GameObject.FindGameObjectWithTag("BombManager").GetComponent<BombManager>();
        _cacheObjectHolder = (CacheObjectHolder)CacheObjectHolder.Instance;
        _sceneLoader = (SceneLoader)SceneLoader.Instance;
        _scoreManager = (ScoreManager)ScoreManager.Instance;

        _touchManager.OnBombTouch += BombTouch;
        _fruitManager.OnMissFruit += MissFruit;
    }

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            QualitySettings.vSyncCount = 0; 
            Application.targetFrameRate = 60;
        }
        Unpaused();
    }

    public void MissFruit()
    {
        _scoreManager.IncreaseMissFruitScore(1);
        if (_scoreManager.CheckIfGameOverByLoseFruit())
        {
            GameOver();
        }
        Debug.Log("Game over: Miss Fruit");
    }

    public void BombTouch()
    {
        Debug.Log("Game over: BombTouch");
        GameOver();
    }

    private void GameOver()
    {
        _sceneLoader.LoadMenuScene();
        Pause();
    }

    private void OnDisable()
    {
        _touchManager.OnBombTouch -= BombTouch;
        _fruitManager.OnMissFruit -= MissFruit;
    }

    private void Pause()
    {
        _fruitManager.gameObject.SetActive(false);
        _touchManager.gameObject.SetActive(false);
        _bombManager.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    private void Unpaused()
    {
        _fruitManager.gameObject.SetActive(true);
        _touchManager.gameObject.SetActive(true);
        _bombManager.gameObject.SetActive(true);

        Time.timeScale = 1;
    }
}
