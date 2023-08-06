using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Timeline.Actions;
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
    [SerializeField] public Animator animator;

    public delegate void SpawnAction();
    public static event SpawnAction OnSpawned;

    private bool isSelctedVar = false;
    public bool IsSelected { get { return isSelctedVar; } private set { isSelctedVar = value; } }

    public GameObject building { get; private set; }

    private MainManager mainManager;

    private void Start()
    {
        mainManager = MainManager.Instance;
        animator = GetComponent<Animator>();
    }

    public void SpawnBuilding(string buildingType)
    {
        if (building != null) return;

        int price = 0;
        GameObject buildingPrefab = null;

        if (buildingType == "Factory")
        {
            price = mainManager.factoryPrice;
            buildingPrefab = factoryPrefab;
        }

        if (buildingType == "Rock")
        {
            price = mainManager.rockPrice;
            buildingPrefab = rockPrefab;
        }

        if (mainManager.Resource1 >= price)
        {
            building = Instantiate(buildingPrefab, spawnPoint.position, Quaternion.identity);
            mainManager.Resource1 -= price;
            OnSpawned?.Invoke();
        }
    }


    public void DestroyBuilding()
    {
        if (building != null)
        {
            Destroy(building);
            building = null;
        }
    }

    public void Deselect()
    {
        selectedMesh.SetActive(false);
        IsSelected = false;
    }

    public void Select()
    {
        if (building)
        {
            if (building.GetComponent<Rock>()) return;
        }
        if (IsSelected)
        {
            if (building == null)
            {
                SpawnBuilding("Factory");
            }

            else
            {
                building.TryGetComponent<Factory>(out Factory factory);
                if (factory != null)
                {
                    factory.DemountBuilding();
                }
            }
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
