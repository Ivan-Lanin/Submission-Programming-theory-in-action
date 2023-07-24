using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] private TMP_Text resource1Text;
    [SerializeField] private TMP_Text resource2Text;
    public int factoryPrice { get { return 20; } }
    public int currentLineNumber;
    private int workerHousePrice { get { return 10; } }

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

    private int resourse2;
    public int Resource2
    {
        get { return resourse2; }
        set
        {
            if (value >= 0)
            {
                resourse2 = value;
                resource2Text.text = resourse2.ToString();
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
        currentLineNumber = 1;
        Resource1 = 20;
        Resource2 = 1;
    }
}
