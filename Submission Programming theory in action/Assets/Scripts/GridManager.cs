using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GridManager : MonoBehaviour
{
    private GridCell[] gridCells;
    private GridCell[] cellLine;
    private MainManager mainManager;

    private int numberOfMrteorsToSpawn = 3;

    void Start()
    {
        gridCells = GetComponentsInChildren<GridCell>();
        mainManager = MainManager.Instance;
        for (int i = 0; i <= numberOfMrteorsToSpawn; i++)
        {
            StartCoroutine(ChooseMeteorTarget());
        }
        FindLines();
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

        Debug.Log(numberOfFactories);

        if (numberOfFactories >= cellLine.Length)
        {
            // TODO: Add shield building
            mainManager.currentLineNumber++;
            FindLines();
            Debug.Log("Line is full");
        }
    }
}
