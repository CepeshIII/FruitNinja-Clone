using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICacheObject
{
    public void Activate(string name, Vector3 position);
    public void Deactivate();
    public void Destroy();

    public bool IsActive();
}
