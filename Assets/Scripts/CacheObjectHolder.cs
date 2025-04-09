using System.Collections.Generic;
using UnityEngine;

public class CacheObjectHolder: Singleton<CacheObjectHolder>
{
    [SerializeField] protected Transform _cacheObjectParent;

    private Dictionary<string, List<ICacheObject>> _initializedCacheObjectLists;


    public ICacheObject GetCacheObject(GameObject prefab, Vector3 position)
    {
        if (_initializedCacheObjectLists == null) 
        {
            _initializedCacheObjectLists = new();
        }

        if (!_initializedCacheObjectLists.TryGetValue(prefab.name, out var list))
        {
            list = new();
            _initializedCacheObjectLists.Add(prefab.name, list);
        }

        if (TryFindInactiveCacheObject(list, out var cacheObject))
        {
            ActivateCacheObject(cacheObject, position, prefab.name);
        }
        else
        {
            cacheObject = InstantiateCacheObject(position, prefab);
            list.Add(cacheObject);
        }

        return cacheObject;
    }

    public ICacheObject InstantiateCacheObject(Vector3 position, GameObject prefab)
    {
        var cacheObject = Instantiate(prefab, _cacheObjectParent).GetComponent<ICacheObject>();
        ActivateCacheObject(cacheObject, position, prefab.name);

        return cacheObject;
    }

    private void ActivateCacheObject(ICacheObject cacheObject, Vector3 position, string type)
    {
        cacheObject.Activate($"CacheObject: {type}", position);
    }

    private bool TryFindInactiveCacheObject(List<ICacheObject> cacheObjects,
        out ICacheObject foundCacheObject)
    {
        foundCacheObject = null;
        foreach (var cacheObject in cacheObjects)
        {
            if (!cacheObject.IsActive())
            {
                foundCacheObject = cacheObject;
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        if (_initializedCacheObjectLists == null) return;

        foreach (var key in _initializedCacheObjectLists.Keys)
        {
            foreach (var cacheObject in _initializedCacheObjectLists[key])
            {
                cacheObject.Destroy();
            }
        }
        _initializedCacheObjectLists.Clear();

        //while(transform.childCount != 0)
        //{
        //    Destroy(transform.GetChild(0));
        //}
    }

    private void OnDisable()
    {
        Clear();
    }
}
