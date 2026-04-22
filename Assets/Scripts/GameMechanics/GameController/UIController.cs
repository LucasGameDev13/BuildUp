using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;
    private event Action<int> OnScoreChanged;

    private void OnEnable()
    {
        OnScoreChanged -= UpdateScore;
        OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        OnScoreChanged -= UpdateScore;
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
}
