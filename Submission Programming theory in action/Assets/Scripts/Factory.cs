using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : BaseBuilding
{
    [SerializeField] Slider slider;

    public MainManager mainManager;
    private int productionSpeed = 20;
    private int productionPower = 2;

    public override int price { get { return 20; } }

    public void Awake()
    {
        StartCoroutine(FactoryProgress());
        mainManager = MainManager.Instance;
        Upgrade();
        Debug.Log("Factory level: " + level);
    }

    IEnumerator FactoryProgress()
    {
        yield return new WaitForSeconds(1f);
        slider.value += productionSpeed;
        if (slider.value >= 100)
        {
            slider.value = 0;
            mainManager.Resource1 += productionPower;
        }
        StartCoroutine(FactoryProgress());
    }
}
