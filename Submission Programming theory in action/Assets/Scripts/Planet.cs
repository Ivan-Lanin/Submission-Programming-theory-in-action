using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Planet : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TMP_Text planetNameText;
    [SerializeField] private Canvas canvas;
    private string planetName;

    string[] planetFirstNames =
    {
        "Straturn", "Uranders", "Toba", "Sirildar", "Kormand", "Astrion", "Xelara Prime", "Nebulon", "Zypheria", "Quasarion", "Vortron", "Celestia", "Luminara",
        "Eclipseia", "Starrendor", "Galactron", "Nyxara", "Zephyria", "Novaris", "Aerion", "Titanus", "Aurelia", "Stellara", "Arcadia", "Eridanus", "Helios", "Prime",
        "Terra", "Nova", "Cosmosis", "Andromeda", "Avalon", "Hyperion", "Lunaris", "Aegis", "Polaris"
    };
    string[] planetMiddleNames =
    {
        "alfa", "betta", "sigma", "gamma", "delta", "epsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu", "nu", "xi", "omicron", "pi", "rho", "sigma",
    };
    string[] planetLastNames =
    {
        "Straturn", "Uranders", "Toba", "Sirildar", "Kormand", "Astrion", "Xelara Prime", "Nebulon", "Zypheria", "Quasarion", "Vortron", "Celestia", "Luminara",
        "Eclipseia", "Starrendor", "Galactron", "Nyxara", "Zephyria", "Novaris", "Aerion", "Titanus", "Aurelia", "Stellara", "Arcadia", "Eridanus", "Helios", "Prime",
        "Terra", "Nova", "Cosmosis", "Andromeda", "Avalon", "Hyperion", "Lunaris", "Aegis", "Polaris"
    };

    // Start is called before the first frame update
    void Start()
    {
        planetName = planetFirstNames[Random.Range(0, planetFirstNames.Length)] + " / " +
                     planetMiddleNames[Random.Range(0, planetMiddleNames.Length)] + " / " +
                     planetLastNames[Random.Range(0, planetLastNames.Length)];
        planetNameText.text = planetName;

        _renderer.material.SetColor("Random", Random.ColorHSV());
        _renderer.material.color = Random.ColorHSV();

        float rotationX = Random.Range(-100f, 100f);
        float rotationY = Random.Range(-100f, 100f);
        float rotationZ = Random.Range(-100f, 100f);
        transform.transform.Rotate(rotationX, rotationY, rotationZ);
    }

    // Update is called once per frame
    void Update()
    {
        int rotationDirection = Random.Range(-1, 1);
        transform.transform.Rotate(0, 0.1f * rotationDirection, 0);
    }

    private void LateUpdate()
    {
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position);
        canvas.transform.position = transform.position + new Vector3(0, 0.37f, -1.52f);
    }

    public void Select()
    {
        SceneManager.LoadScene("MainScene");
    }
}
