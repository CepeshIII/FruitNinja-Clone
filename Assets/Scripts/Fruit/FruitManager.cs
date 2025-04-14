using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public delegate void FruitManagerEvent();

public class FruitManager: MonoBehaviour 
{
    public FruitManagerEvent OnMissFruit;
    public FruitManagerEvent OnFruitSlice;

    [SerializeField] private List<GameObject> fruitPrefabs;
    [SerializeField] private CacheObjectHolder cacheObjectHolder;
    [SerializeField] private Thrower thrower;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private ScoreManager scoreManager;


    [SerializeField] private float timeBetweenSpawnFruit = 1f;

    public void OnEnable()
    {
        cacheObjectHolder = (CacheObjectHolder)CacheObjectHolder.Instance;
        thrower = (Thrower)Thrower.Instance;
        soundManager = (SoundManager)SoundManager.Instance;
        scoreManager = (ScoreManager)ScoreManager.Instance;
        //cacheObjectHolder.Clear();
        StartCoroutine(TimerForSpawnFruit());
    }

    public void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space)) 
        //{
        //    SpawnFruit();
        //}
    }

    public IEnumerator TimerForSpawnFruit()
    {
        while (true)
        {
            SpawnFruit();
            yield return new WaitForSeconds(timeBetweenSpawnFruit);
        }
    }

    public void SpawnFruit()
    {
        var cacheObject = cacheObjectHolder.GetCacheObject(
        fruitPrefabs[Random.Range(0, fruitPrefabs.Count)],
        Vector3.zero);

        var fruit = (Fruit)cacheObject;
        fruit.OnWholeFruitFall += MissFruit;
        fruit.OnFruitSlice += SliceFruit;

        soundManager.PlayTossSound();
        thrower.Throw(fruit.WholeFruit.Rigidbody);
    }

    public void SliceFruit()
    {
        soundManager.PlayFruitSound();
        scoreManager.IncreaseScore(1);
    }

    public void MissFruit()
    {
        OnMissFruit.Invoke();
    }
}
