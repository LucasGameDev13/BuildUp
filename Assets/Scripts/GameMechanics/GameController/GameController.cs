using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    private SpawnerController spawnerController;
    private SpawnerHighController spawnerHighController;
    private UIController uiController;
    private SoundController soundController;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    public SoundController SoundController => soundController;

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

        spawnerController = GetComponentInChildren<SpawnerController>();
        spawnerHighController = GetComponentInChildren<SpawnerHighController>();
        uiController = GetComponentInChildren<UIController>();
        soundController = GetComponentInChildren<SoundController>();
    }


    public void ReleaseCubeEvents()
    {
        uiController.TriggerScoreChange(1);
        spawnerController.TriggerReleaseCube();
    }

}
