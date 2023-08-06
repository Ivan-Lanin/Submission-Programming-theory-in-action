using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : BaseBuilding
{
    [SerializeField] Slider productionProgressSlider;

    private int productionSpeed = 20;
    private int productionPower = 5;

    private int autoProductionCount;
    public int AutoProductionCount { get { return autoProductionCount; } private set { } }

    private int MaxAutoProductionCount = 1;

    public delegate void FactoryAction();
    public static event FactoryAction OnFactoryDestroyed;



    public override int price { get { return 20; } }

    public override void Awake()
    {
        base.Awake();
        StartCoroutine(FactoryProgress());
        ResetAutoProduction();
    }

    IEnumerator FactoryProgress()
    {
        yield return new WaitForSeconds(1f);
        productionProgressSlider.value += productionSpeed;
        if (productionProgressSlider.value >= 100)
        {
            productionProgressSlider.value = 0;
            mainManager.Resource1 += productionPower;
            autoProductionCount--;
        }
        
        if (autoProductionCount > 0)
        {
            StartCoroutine(FactoryProgress());
        }
        else
        {
            productionProgressSlider.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(FactoryProgress());
        if (!isDemounted)
        {
            mainManager.Resource2 += 1;
            OnFactoryDestroyed?.Invoke();
        }
    }

    public void ResetAutoProduction()
    {
        productionProgressSlider.gameObject.SetActive(true);
        autoProductionCount = MaxAutoProductionCount;
        StartCoroutine(FactoryProgress());
    }
}
