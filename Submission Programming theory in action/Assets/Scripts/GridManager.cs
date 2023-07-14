using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GridManager : MonoBehaviour
{
    private GridCell[] gridCells;
    private MainManager mainManager;

    void Start()
    {
        gridCells = GetComponentsInChildren<GridCell>();
        mainManager = MainManager.Instance;
        StartCoroutine(ChooseMeteorTarget());
    }

    public void DeselectAll()
    {
        foreach (GridCell cell in gridCells)
        {
            cell.Deselect();
        }
    }

    public void BuildButtonClicked()
    {
        foreach (GridCell cell in gridCells)
        {
            if (cell.IsSelected)
            {
                cell.SpawnBuilding();
            }
        }
    }

    public void DestroyButtonClicked()
    {
        foreach (GridCell cell in gridCells)
        {
            if (cell.IsSelected)
            {
                cell.DemountBuilding();
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
}
