using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material deselectedMaterial;
    [SerializeField] private Material alertMaterial;
    [SerializeField] private GameObject factoryPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool isSelctedVar = false;
    public bool IsSelected { get { return isSelctedVar; } private set { isSelctedVar = value; } }

    private GameObject building;

    private MainManager mainManager;

    private void Start()
    {
        mainManager = MainManager.Instance;
    }

    public void SpawnBuilding()
    {
        if (mainManager.Resource1 >= 10)
        {
            building = Instantiate(factoryPrefab, spawnPoint.position, Quaternion.identity);
            mainManager.Resource1 -= 10;
        }
    }

    public void DemountBuilding() 
    {
        if (building != null)
        {
            Destroy(building);
            mainManager.Resource1 += 10;
        }
    }

    public void DestroyBuilding()
    {
        if (building != null)
        {
            Destroy(building);
        }
    }

    public void Deselect()
    {
        GetComponentInChildren<MeshRenderer>().material = deselectedMaterial;
        IsSelected = false;
    }

    public void Select()
    {
        GetComponentInChildren<MeshRenderer>().material = selectedMaterial;
        IsSelected = true;
    }

    public void MeteorAlert()
    {
        GetComponentInChildren<MeshRenderer>().material = alertMaterial;
        StartCoroutine("MeteorCountdown");
    }

    IEnumerator MeteorCountdown()
    {
        yield return new WaitForSeconds(5);
        GetComponentInChildren<MeshRenderer>().material = deselectedMaterial;
        if (building != null)
        {
            DestroyBuilding();
        }
    }
}
