using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private SceneController sceneController;
    private GameLevelController gameLevelController;
    private UIController uiController;
    private SoundController soundController;
    private CloudServices cloudServices;


    public SceneController SceneController => sceneController;
    public UIController UIController => uiController;
    public SoundController SoundController => soundController;
    public CloudServices CloudServices => cloudServices;

    public Action OnReleasedCube;


    protected override void Awake()
    {
        base.Awake();
        sceneController = GetComponentInChildren<SceneController>();
        gameLevelController = GetComponentInChildren<GameLevelController>();
        uiController = GetComponentInChildren<UIController>();
        soundController = GetComponentInChildren<SoundController>();
        cloudServices = GetComponent<CloudServices>();
    }

    private void OnEnable()
    {
        if (sceneController != null) sceneController.OnSceneLoaded += HandleSceneLoaded;

        if (uiController == null) return;

        uiController.SetButtonFunctionality(UIButtonType.PlayButton, () => PlayGame());
        uiController.SetButtonFunctionality(UIButtonType.HomeButton, () => HomeGame());
        uiController.SetButtonFunctionality(UIButtonType.RankingButton, async () => { uiController.SetupRankingScreen(); await uiController.SetupRankingText(cloudServices);});
        uiController.SetButtonFunctionality(UIButtonType.UserButton, () => uiController.SetupUserScreen());
        uiController.SetButtonFunctionality(UIButtonType.AcceptButton, () => uiController.CloseCurrentPopup());
        uiController.SetButtonFunctionality(UIButtonType.BackButton, () => uiController.CloseCurrentPopup());
        uiController.SetButtonFunctionality(UIButtonType.CancelButton, () => uiController.CloseCurrentPopup());
        uiController.SetButtonFunctionality(UIButtonType.QuitButton, () => gameLevelController.QuitGame());
    }

    private void Start()
    {
        HandleSceneLoaded(sceneController.MySceneIndex());
        cloudServices.InitializeLoginConnectionVerification();
    }

    private void OnDisable()
    {
        if (sceneController != null) sceneController.OnSceneLoaded -= HandleSceneLoaded;

        if (uiController == null) return;

        uiController.RemoveButtonFunctionality(UIButtonType.PlayButton);
        uiController.RemoveButtonFunctionality(UIButtonType.HomeButton);
        uiController.RemoveButtonFunctionality(UIButtonType.RankingButton);
        uiController.RemoveButtonFunctionality(UIButtonType.UserButton);
        uiController.RemoveButtonFunctionality(UIButtonType.AcceptButton);
        uiController.RemoveButtonFunctionality(UIButtonType.BackButton);
        uiController.RemoveButtonFunctionality(UIButtonType.CancelButton);
        uiController.RemoveButtonFunctionality(UIButtonType.QuitButton);
    }

    private void HandleSceneLoaded(int sceneIndex)
    {
        gameLevelController.InitializeGame(sceneIndex);

        switch (sceneIndex)
        {
            case 0:
                uiController.SetupHomeScreen();
                soundController.PlayBackgroundMusicHome();
                break;

            case 1:
                uiController.SetupScoreScreen();
                OnReleasedCube?.Invoke();
                soundController.PlayBackgroundMusicGamePlay();
                break;
        }
    }


    private void PlayGame()
    {
        gameLevelController.PlayGame();
    }

    private void HomeGame()
    {
        gameLevelController.ReturnHome();
        cloudServices.InitializeLoginConnectionVerification();
    }

    public void ReleaseCubeEvents()
    {
        gameLevelController.TriggerScoreChange();
        OnReleasedCube?.Invoke();
    }

    public void ReleaseGameOver()
    {
        gameLevelController.TriggerGameOver(true);
    }

    public float GetGameOverDelay()
    {
        return gameLevelController.GameOverDelay;
    }
}
