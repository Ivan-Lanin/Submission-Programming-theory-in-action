using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuilding : MonoBehaviour
{
    public abstract int price { get; }
    public int level = 1;

    public virtual void Upgrade()
    {
        level++;
    }

    public virtual void DemountBuilding()
    {
        MainManager.Instance.Resource1 += price;
        Destroy(gameObject);
    }
}
