using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BombManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bombPrefabs;
    [SerializeField] private CacheObjectHolder cacheObjectHolder;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Thrower thrower;

    [SerializeField] private float timeBetweenSpawn = 1f;


    public void OnEnable()
    {
        cacheObjectHolder = (CacheObjectHolder)CacheObjectHolder.Instance;
        thrower = (Thrower)Thrower.Instance;
        soundManager = (SoundManager)SoundManager.Instance;

        StartCoroutine(TimerForSpawn());
    }


    public IEnumerator TimerForSpawn()
    {
        while (true)
        {
            var bomb = (Bomb)cacheObjectHolder.GetCacheObject(
                bombPrefabs[Random.Range(0, bombPrefabs.Count)],
                Vector3.zero);

            bomb.OnBombExplode += BombExplosion;
            thrower.Throw(bomb.Rigidbody);

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private void BombExplosion()
    {
        soundManager.PlayBombExplosionSound();
    }
}
