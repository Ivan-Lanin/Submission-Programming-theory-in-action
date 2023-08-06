using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerZone : MonoBehaviour
{
    [SerializeField] private Renderer towerZoneRenderer;

    public void Enable()
    {
        towerZoneRenderer.enabled = true;
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        towerZoneRenderer.enabled = false;
    }
}
