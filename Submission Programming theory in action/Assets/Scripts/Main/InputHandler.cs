using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                if (cell != null)
                {
                    gridManager.DeselectAll(cell);
                    cell.Select();
                }
            }
            else
            {
                gridManager.DeselectAll();
            }
        }

        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                cell.animator.SetTrigger("popTrigger");
                if (cell.building == null) return;

                cell.building.TryGetComponent<Factory>(out Factory factory);
                if (factory == null) return;
                if (factory.AutoProductionCount <= 0)
                {
                    factory.ResetAutoProduction();
                }
            }
        }
    }
}
