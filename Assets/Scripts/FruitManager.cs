using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public delegate void FruitManagerEvent();

public class FruitManager: MonoBehaviour 
{
    public FruitManagerEvent OnMissFruit;

    [SerializeField] private List<GameObject> fruitPrefabs;
    [SerializeField] private CacheObjectHolder cacheObjectHolder;
    [SerializeField] private Thrower thrower;
    
    [SerializeField] private float timeBetweenSpawnFruit = 1f;

    public void OnEnable()
    {
        cacheObjectHolder = (CacheObjectHolder)CacheObjectHolder.Instance;
        thrower = (Thrower)Thrower.Instance;
        //cacheObjectHolder.Clear();
        StartCoroutine(TimerForSpawnFruit());
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            SpawnFruit();
        }
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

        thrower.Throw(fruit.WholeFruit.Rigidbody);
    }

    public void MissFruit()
    {
        OnMissFruit.Invoke();
    }
}
