using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BaseBuilding
{
    public override int price { get { return 10; } }

    private void OnDestroy()
    {
        mainManager.Resource1 += price;
    }
}
