using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bombPrefabs;
    [SerializeField] private CacheObjectHolder cacheObjectHolder;
    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private Thrower thrower;


    public void OnEnable()
    {
        cacheObjectHolder = (CacheObjectHolder)CacheObjectHolder.Instance;
        thrower = (Thrower)Thrower.Instance;

        StartCoroutine(TimerForSpawn());
    }


    public IEnumerator TimerForSpawn()
    {
        while (true)
        {
            var bomb = (Bomb)cacheObjectHolder.GetCacheObject(
                bombPrefabs[Random.Range(0, bombPrefabs.Count)],
                Vector3.zero);
            thrower.Throw(bomb.Rigidbody);

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
