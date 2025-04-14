using System.Collections.Generic;
using UnityEngine;

public class UICrossManager : MonoBehaviour, IUIIntDisplayer
{
    [SerializeField] private List<UICross> crosses;
    [SerializeField] private UICross crossPrefab;

    public void Init(int maxCount)
    {
        InitializeCrosses(maxCount);
    }

    private void InitializeCrosses(int maxCount)
    {
        if (crosses != null) ClearCrosses();

        crosses = new List<UICross>(maxCount);

        for (int i = 0; i < maxCount; i++)
        {
            crosses.Add(Instantiate(crossPrefab, transform));
        }
    }

    public void UpdateDisplayer(int count)
    {
        UpdateCrosses(count);
    }

    private void UpdateCrosses(int count) 
    {
        count = Mathf.Clamp(count, 0, crosses.Count);
        for (int i = 0; i < count; i++)
        {
            var cross = crosses[i];
            if (cross != null) 
            {
                cross.Trigger();
            }
        }
    }

    private void ClearCrosses()
    {
        foreach (var c in crosses)
        {
            if (c != null)
                Destroy(c.gameObject);
        }
        crosses.Clear();

        var children = GetComponentsInChildren<UICross>();

        foreach (var child in children)
        {
            if (child != null)
                Destroy(child.gameObject);
        }
    }

    private void OnDestroy()
    {
        ClearCrosses();
    }
}
