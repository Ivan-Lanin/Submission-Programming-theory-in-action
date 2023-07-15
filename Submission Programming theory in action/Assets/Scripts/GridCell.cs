using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GridCell : MonoBehaviour
{
    
    [SerializeField] private Material defaulttMaterial;
    [SerializeField] private Material alertMaterial;
    [SerializeField] private GameObject factoryPrefab;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject selectedMesh;
    [SerializeField] private Transform spawnPoint;

    private bool isSelctedVar = false;
    public bool IsSelected { get { return isSelctedVar; } private set { isSelctedVar = value; } }

    public GameObject building { get; private set; }

    private MainManager mainManager;

    private void Start()
    {
        mainManager = MainManager.Instance;
    }

    public void SpawnBuilding(string buildingType)
    {
        int price = 0;
        GameObject buildingPrefab = null;

        if (buildingType == "Factory")
        {
            price = MainManager.Instance.factoryPrice;
            buildingPrefab = factoryPrefab;
        }

        if (mainManager.Resource1 >= price)
        {
            building = Instantiate(buildingPrefab, spawnPoint.position, Quaternion.identity);
            mainManager.Resource1 -= price;
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
        selectedMesh.SetActive(false);
        IsSelected = false;
    }

    public void Select()
    {
        selectedMesh.SetActive(true);
        IsSelected = true;
    }

    public void MeteorAlert()
    {
        GetComponentInChildren<MeshRenderer>().material = alertMaterial;
        StartCoroutine("MeteorCountdown");
    }

    IEnumerator MeteorCountdown()
    {
        yield return new WaitForSeconds(2);
        Instantiate(meteorPrefab, new Vector3(0, 7,0), Quaternion.identity);
        yield return new WaitForSeconds(3);
        GetComponentInChildren<MeshRenderer>().material = defaulttMaterial;
        if (building != null)
        {
            DestroyBuilding();
        }
    }
}
