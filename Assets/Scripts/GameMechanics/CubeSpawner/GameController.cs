using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    private SpawnerController spawnerController;
    private SpawnerHighController spawnerHighController;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    public SpawnerController SpawnerController => spawnerController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        spawnerController = GetComponent<SpawnerController>();
        spawnerHighController = GetComponentInChildren<SpawnerHighController>();
    }

}
