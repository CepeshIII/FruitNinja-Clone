using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitManager: MonoBehaviour
{
    [SerializeField] private List<GameObject> fruitPrefabs;
    [SerializeField] private FruitHolder fruitHolder;
    [SerializeField] private float tossFruitForce = 100f;
    [SerializeField] private float tossFruitTorque = 1;

    [SerializeField] private Bounds bounds = new Bounds(new Vector3(0f, 4f, 0f), new Vector3(4f, 0f, 0f));

    public void OnEnable()
    {
        fruitHolder = (FruitHolder)FindAnyObjectByType(typeof(FruitHolder));
        fruitHolder.Clear();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            var fruit = fruitHolder.SpawnFruit(
                fruitPrefabs[Random.Range(0, fruitPrefabs.Count)], 
                Vector3.zero);

            TossFruit(fruit);
}
    }

    public void TossFruit(Fruit fruit)
    {
        var torqueDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var startPos = Vector3.right * x;



        fruit.TossFruit(startPos, (bounds.center - startPos).normalized * tossFruitForce, torqueDirection * tossFruitTorque);

    }

}
