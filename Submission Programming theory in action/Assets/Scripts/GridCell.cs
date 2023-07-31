using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GridCell : MonoBehaviour
{
    
    [SerializeField] private Material defaulttMaterial;
    [SerializeField] private Material alertMaterial;
    [SerializeField] private GameObject factoryPrefab;
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject shield;
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

        if (buildingType == "Rock")
        {
            price = MainManager.Instance.rockPrice;
            buildingPrefab = rockPrefab;
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
        if (IsSelected) return;
        if (building)
        {
            if (building.GetComponent<Rock>()) return;
        }
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
        Instantiate(meteorPrefab, new Vector3(transform.position.x, 7, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(2.8f);
        GetComponentInChildren<MeshRenderer>().material = defaulttMaterial;
        if (building != null)
        {
            if (!shield.activeSelf)
            {
                DestroyBuilding();
            }
        }
    }

    public GridCell[] CheckMyNeighbours(int lineNumber)
    {
        Collider[] neighbours = Physics.OverlapSphere(transform.position, 1 * lineNumber);

        int cellsInNeighbours = 0;

        foreach (Collider neighbour in neighbours)
        { 
            if (neighbour.GetComponent<GridCell>() != null)
            {
                cellsInNeighbours++;
            }
        }

        GridCell[] cellLines = new GridCell[cellsInNeighbours];
        int i = 0;
        foreach (Collider neighbour in neighbours)
        {
            //if (neighbour.gameObject == gameObject) continue;
            if (neighbour.GetComponent<GridCell>() == null) continue;
            cellLines[i] = neighbour.GetComponent<GridCell>();
            i++;
        }
        return cellLines;
    }

    public void ActivateShield()
    {
        if (shield.activeSelf) return;
        shield.SetActive(true);
    }
}
