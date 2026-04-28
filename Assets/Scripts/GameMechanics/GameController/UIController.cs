using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Game Score Settings")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;
    private event Action<int> OnScoreChanged;

    [Header("UI Game Over Settings")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverFinalScoreText;
    [SerializeField] private float gameOverDelay = 1f;
    private event Action<bool> OnGameOver;

    public float GameOverDelay => gameOverDelay;

    private void Start()
    {
        TriggerGameOverScreen(false);
    }

    private void OnEnable()
    {
        OnScoreChanged -= UpdateScore;
        OnScoreChanged += UpdateScore;

        OnGameOver -= SetupGameOverScreen;
        OnGameOver += SetupGameOverScreen;
    }

    private void OnDisable()
    {
        OnScoreChanged -= UpdateScore;
        OnGameOver -= SetupGameOverScreen;
    }

    private void UpdateScore(int _score)
    {
        currentScore += _score;
        scoreText.text = currentScore.ToString();
    }
    public void TriggerScoreChange(int _score)
    {
        OnScoreChanged?.Invoke(_score);
    }

    private void SetupGameOverScreen(bool _isGameOver)
    {
        if (_isGameOver) { GameController.Instance.SoundController.PlayGameOverSound();}
        Time.timeScale = _isGameOver ? 0f : 1f;
        scorePanel.SetActive(!_isGameOver);
        gameOverPanel.SetActive(_isGameOver);
        gameOverFinalScoreText.text = "Score: " + scoreText.text;
    }

    public void TriggerGameOverScreen(bool _isGameOver)
    {
        OnGameOver?.Invoke(_isGameOver);
    }
}
