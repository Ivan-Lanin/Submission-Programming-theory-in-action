using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerZoneSelector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.name);
                hit.collider.TryGetComponent<TowerZone>(out TowerZone towerZone);
                if (towerZone != null)
                {
                    hit.collider.GetComponent<TowerZone>().Enable();
                }
            }
        }
    }
}
