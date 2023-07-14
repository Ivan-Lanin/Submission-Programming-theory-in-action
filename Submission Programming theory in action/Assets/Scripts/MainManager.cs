using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] private TMP_Text resource1Text;

    public static MainManager Instance { get; private set; }
    public bool isGameActive { get; internal set; }
    private int resourse1;
    public int Resource1 
    {
        get { return resourse1; }
        set 
        {
            if (value >= 0)
            {
                resourse1 = value;
                resource1Text.text = resourse1.ToString();
            }
        } 
    }

    private void Awake()
    {
        Instance = this;
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        isGameActive = true;
        Resource1 = 10;
    }
}
