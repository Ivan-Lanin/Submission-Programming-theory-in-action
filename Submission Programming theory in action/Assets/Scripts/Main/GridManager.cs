using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GridManager : MonoBehaviour
{
    private GridCell[] gridCells;
    private GridCell[] cellLine;
    private MainManager mainManager;

    private int numberOfMrteorsToSpawn = 10;
    private int numberOfRocksToSpawn = 6;

    void OnEnable()
    {
        GridCell.OnSpawned += CheckForTheLineProgress;
    }

    void OnDisable()
    {
        GridCell.OnSpawned -= CheckForTheLineProgress;
    }

    void Start()
    {
        gridCells = GetComponentsInChildren<GridCell>();
        mainManager = MainManager.Instance;

        for (int i = 0; i < numberOfRocksToSpawn; i++)
        {
            SpawnRocks();
        }

        for (int i = 0; i <= numberOfMrteorsToSpawn; i++)
        {
            StartCoroutine(ChooseMeteorTarget());
        }

        FindLines();
    }

    private void SpawnRocks()
    {
        int cellIndex = Random.Range(0, gridCells.Length);
        gridCells[cellIndex].SpawnBuilding("Rock");
    }

    public void DeselectAll(GridCell selectedCell)
    {
        foreach (GridCell cell in gridCells)
        {
            if (selectedCell == cell) continue;
            cell.Deselect();
        }
    }

    public void DeselectAll()
    {
        foreach (GridCell cell in gridCells)
        {
            cell.Deselect();
        }
    }

    public void BuildFactoryButtonClicked()
    {
        foreach (GridCell cell in gridCells)
        {
            if (cell.IsSelected)
            {
                cell.SpawnBuilding("Factory");
                CheckForTheLineProgress();
            }
        }
    }

    public void DestroyButtonClicked()
    {
        foreach (GridCell cell in gridCells)
        {
            if (!cell.IsSelected || cell.building == null) continue;

            cell.building.TryGetComponent<Factory>(out Factory factory);
            if (factory != null)
            {
                factory.DemountBuilding();
                CheckForTheLineProgress();
            }
        }
    }

    IEnumerator ChooseMeteorTarget()
    {
        while (mainManager.isGameActive)
        {
            int cellIndex = Random.Range(0, gridCells.Length);
            gridCells[cellIndex].MeteorAlert();
            yield return new WaitForSeconds(5);
            CheckForTheLineProgress();
        }
    }

    private void FindLines()
    {
        foreach (GridCell cell in gridCells)
        {
            if (cell.name == "Grid cell")
            {
                cellLine = cell.CheckMyNeighbours(mainManager.currentLineNumber);
            }
        }
    }   

    private void CheckForTheLineProgress()
    {
        int numberOfFactories = 0;
        if (cellLine == null) return;

        foreach (GridCell cell in cellLine)
        {
            if (cell.building != null)
            {
                cell.building.TryGetComponent<Factory>(out Factory factory);
                if (factory != null)
                {
                    numberOfFactories++;
                }
            }
        }


        if (numberOfFactories >= cellLine.Length)
        {
            if (mainManager.currentLineNumber == 3)
            {
                Debug.Log("The floar is completed");
            }

            foreach (GridCell cell in cellLine)
            {
                cell.ActivateShield();
            }
            mainManager.currentLineNumber++;
            FindLines();
            Debug.Log("Line is full");
        }
    }
}
