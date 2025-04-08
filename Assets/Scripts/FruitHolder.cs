using System.Collections.Generic;
using UnityEngine;

public class FruitHolder: Singleton
{
    [SerializeField] protected Transform _fruitParent;

    private Dictionary<string, List<Fruit>> _initializedFruitLists;

    public Fruit SpawnFruit(GameObject prefab, Vector3 position)
    {
        if (_initializedFruitLists == null) 
        {
            _initializedFruitLists = new();
        }

        if (!_initializedFruitLists.TryGetValue(prefab.name, out var list))
        {
            list = new();
            _initializedFruitLists.Add(prefab.name, list);
        }

        if (TryFindInactiveFruit(list, out var fruit))
        {
            ActivateFruit(fruit, position, prefab.name);
        }
        else
        {
            fruit = InstantiateFruit(position, prefab);
            list.Add(fruit);
        }

        return fruit;
    }

    public Fruit InstantiateFruit(Vector3 position, GameObject prefab)
    {
        var fruit = Instantiate(prefab, _fruitParent).GetComponent<Fruit>();
        ActivateFruit(fruit, position, prefab.name);

        return fruit;
    }

    private void ActivateFruit(Fruit fruit, Vector3 position, string type)
    {
        fruit.Activate();
        fruit.name = $"Fruit: {type}";
        fruit.transform.position = position + Vector3.up;
    }

    private bool TryFindInactiveFruit(List<Fruit> fruits,
        out Fruit foundFruit)
    {
        foundFruit = null;
        foreach (var fruit in fruits)
        {
            if (!fruit.gameObject.activeSelf)
            {
                foundFruit = fruit;
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        if (_initializedFruitLists == null) return;

        foreach (var key in _initializedFruitLists.Keys)
        {
            foreach (var fruit in _initializedFruitLists[key])
            {
                Destroy(fruit);
            }
        }
        _initializedFruitLists.Clear();

        while(transform.childCount != 0)
        {
            Destroy(transform.GetChild(0));
        }
        
    }
}
