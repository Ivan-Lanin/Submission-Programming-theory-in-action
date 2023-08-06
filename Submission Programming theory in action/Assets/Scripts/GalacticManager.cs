using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SpawnModes
{
    Spawn,
    Respawn
}

public class GalacticManager : MonoBehaviour
{
    [SerializeField] Transform planet1SpawnPosition;
    [SerializeField] Transform planet2SpawnPosition;
    [SerializeField] Transform planet3SpawnPosition;
    [SerializeField] Transform planet4SpawnPosition;
    [SerializeField] GameObject planetPrefab;

    private GameObject planet1;
    private GameObject planet2;
    private GameObject planet3;
    private GameObject planet4;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlanets();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Planet planet = hit.collider.GetComponent<Planet>();
                if (planet)
                {
                    planet.Select();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnPlanets(SpawnModes.Respawn);
        }
    }

    private void SpawnPlanets(SpawnModes mode = SpawnModes.Spawn)
    {
        if (mode == SpawnModes.Respawn)
        {
            Destroy(planet1);
            Destroy(planet2);
            Destroy(planet3);
            Destroy(planet4);
        }
        planet1 = Instantiate(planetPrefab, planet1SpawnPosition.position, Quaternion.identity);
        planet2 = Instantiate(planetPrefab, planet2SpawnPosition.position, Quaternion.identity);
        planet3 = Instantiate(planetPrefab, planet3SpawnPosition.position, Quaternion.identity);
        planet4 = Instantiate(planetPrefab, planet4SpawnPosition.position, Quaternion.identity);
    }
}
