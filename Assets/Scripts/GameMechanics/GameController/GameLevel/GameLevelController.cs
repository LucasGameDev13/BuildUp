using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLevelController : MonoBehaviour
{
    private const int HomeSceneIndex = 0;
    private const int GameplaySceneIndex = 1;

    [Header("Scene Score Settings")]
    [SerializeField] private int scorePerCube = 10;
    private int currentScore = 0;
    private event Action OnScoreChanged;

    [Header("Scene Game Over Settings")]
    [SerializeField] private float gameOverDelay = 1f;
    private event Action<bool> OnGameOver;

    public float GameOverDelay => gameOverDelay;


    private void OnEnable()
    {
        OnScoreChanged -= UpdateScore;
        OnScoreChanged += UpdateScore;

        OnGameOver -= GameOver;
        OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        OnScoreChanged -= UpdateScore;
        OnGameOver -= GameOver;
    }






    public void InitializeGame(int _sceneIndex)
    {
        if (_sceneIndex == HomeSceneIndex)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void PlayGame()
    {
        ResetScore();
        ChangeScene(GameplaySceneIndex);
    }

    private void UpdateScore()
    {
        currentScore += scorePerCube;
        GameController.Instance.UIController.SetupScoreText(currentScore);
    }

    private void GameOver(bool _isGameOver = false)
    {
        if (!_isGameOver) { return; }
        GameController.Instance.SoundController.PlayGameOverSound();
        GameController.Instance.UIController.SetupGameOverScoreText();
        GameController.Instance.UIController.SetupGameOverScreen();
        GameController.Instance.CloudServices.SaveScore(currentScore);
        Time.timeScale = 0f;
    }
    public void ReturnHome()
    {
        ChangeScene(HomeSceneIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void ChangeScene(int index)
    {
        GameController.Instance.SceneController.ChangeScene(index);
    }

    private void ResetScore()
    {
        currentScore = 0;
        GameController.Instance.UIController.SetupScoreText(currentScore);
    }

    public void TriggerScoreChange()
    {
        OnScoreChanged?.Invoke();
    }
    public void TriggerGameOver(bool _isGameOver)
    {
        OnGameOver?.Invoke(_isGameOver);
    }
}
