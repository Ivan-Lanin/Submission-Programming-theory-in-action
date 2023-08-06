using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPlanet : MonoBehaviour
{

    [SerializeField] private GameObject towerZonePrefab;

    void Start()
    {
        SpawnTowerZones();
    }

    private void SpawnTowerZones()
    {
        int angle = 30;
        for (int i = 0; i < 11; i++)
        {
            GameObject towerZone = Instantiate(towerZonePrefab, transform);
            towerZone.transform.localPosition = new Vector3(0, 0, 0);
            towerZone.transform.localRotation = Quaternion.Euler(0, 0, 0 + angle);
            angle += 30;
        }

        angle = 30;
        for (int i = 0; i < 11; i++)
        {
            if (i == 5)
            {
                angle += 30;
                continue;
            }
            GameObject towerZone = Instantiate(towerZonePrefab, transform);
            towerZone.transform.localPosition = new Vector3(0, 0, 0);
            towerZone.transform.localRotation = Quaternion.Euler(0 + angle, 0, 0);
            angle += 30;
        }
    }
}
