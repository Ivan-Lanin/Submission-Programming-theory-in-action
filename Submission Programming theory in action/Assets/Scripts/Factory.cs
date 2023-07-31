using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : BaseBuilding
{
    [SerializeField] Slider productionProgressSlider;

    private int productionSpeed = 20;
    private int productionPower = 20;

    public override int price { get { return 20; } }

    public override void Awake()
    {
        base.Awake();
        StartCoroutine(FactoryProgress());
    }

    IEnumerator FactoryProgress()
    {
        yield return new WaitForSeconds(1f);
        productionProgressSlider.value += productionSpeed;
        if (productionProgressSlider.value >= 100)
        {
            productionProgressSlider.value = 0;
            mainManager.Resource1 += productionPower;
        }
        StartCoroutine(FactoryProgress());
    }
}
