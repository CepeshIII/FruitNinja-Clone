using System;
using UnityEngine;

public class MissFruitDisplayer: MonoBehaviour
{
    private IUIIntDisplayer displayer;

    private int maxValue = 0;
    private int currentValue = 0;

    public void OnEnable()
    {
        displayer = GetComponent<IUIIntDisplayer>();
    }

    public void Display(int maxMissFruitCount, int missFruitScore)
    {
        if (displayer == null) return;

        if(maxMissFruitCount > maxValue)
            displayer.Init(maxMissFruitCount);

        displayer.UpdateDisplayer(missFruitScore);

        maxValue = maxMissFruitCount;
    }
}