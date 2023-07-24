using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : BaseBuilding
{
    [SerializeField] Slider shieldHealthSlider;

    public override int price { get { return 100; } }

    public override void Awake()
    {
        base.Awake();
    }
}
