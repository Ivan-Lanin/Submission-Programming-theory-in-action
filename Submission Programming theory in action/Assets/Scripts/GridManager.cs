using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GridCell[] gridCells;

    void Start()
    {
        gridCells = GetComponentsInChildren<GridCell>();
    }
}
