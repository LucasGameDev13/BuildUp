using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Home Screen Settings")]
    [SerializeField] private GameObject homePanel;
    [SerializeField] private TextMeshProUGUI homeRecordScoreText;
    
    [Header("UI User Settings")]
    [SerializeField] private GameObject userPanel;
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TMP_InputField userNameInputField;

    [Header("UI Game Score Settings")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("UI Ranking Settings")]
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private List<TextMeshProUGUI> rankingScoreText = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> rankingNameText = new List<TextMeshProUGUI>();

    [Header("UI Game Over Settings")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverFinalScoreText;

    [Header("UI Buttons Settings")]
    [SerializeField] private List<UIButtonController> buttons = new List<UIButtonController>();

    private GameObject currentScreen;
    private GameObject currentPopup;

    public GameObject UserPanel { get { return userPanel; } }
    public GameObject RankingPanel { get { return rankingPanel; } }

    public void SetupHomeScreen()
    {
        OpenScreen(homePanel);
    }

    public void SetupUserScreen()
    {
        OpenPopup(userPanel);
    }

    public void SetupRankingScreen()
    {
        OpenPopup(rankingPanel);
    }

    public void SetupScoreScreen()
    {

        OpenScreen(scorePanel);
    }

    public void SetupGameOverScreen()
    {
        OpenScreen(gameOverPanel);
    }


    public void SetupScoreText(int _scoreValue)
    {
        scoreText.text = _scoreValue.ToString();
    }

    public void SetupRankingText()
    {

    }

    public void SetupUserNameText(string userName)
    {
        userNameText.text = userName;
    }

    public void SetupUserNameInputField(string userName)
    {
        userNameInputField.text = userName;
    }

    public void SetupGameOverScoreText()
    {
        gameOverFinalScoreText.text = "Score: " + scoreText.text;
    }


    public void OpenScreen(GameObject screen)
    {
        if (currentScreen != null && currentScreen != screen)
        {
            currentScreen.SetActive(false);
        }

        currentScreen = screen;
        currentScreen.SetActive(true);
    }

    public void OpenPopup(GameObject popup)
    {
        if (currentPopup != null && currentPopup != popup)
        {
            currentPopup.SetActive(false);
        }

        currentPopup = popup;
        currentPopup.SetActive(true);
    }

    public void CloseCurrentPopup()
    {
        if (currentPopup != null)
        {
            currentPopup.SetActive(false);
            currentPopup = null;
        }
    }

    public void SetButtonFunctionality(UIButtonType buttonType, Action _eventAction)
    {
        foreach(var buttonsList in buttons)
        {
            if(buttonsList.GetButtonByType(buttonType) != null)
            {
                buttonsList.GetButtonByType(buttonType).onClick.RemoveAllListeners();
                buttonsList.GetButtonByType(buttonType).onClick.AddListener(() => _eventAction.Invoke());
                buttonsList.GetButtonByType(buttonType).onClick.AddListener(() => GameController.Instance.SoundController.PlayButtonClickSound());
            }
        }
    }

    public void RemoveButtonFunctionality(UIButtonType buttonType)
    {
        foreach (var buttonsList in buttons)
        {
            if (buttonsList.GetButtonByType(buttonType) != null)
            {
                buttonsList.GetButtonByType(buttonType).onClick.RemoveAllListeners();
            }
        }
    }
}

public enum UIButtonType
{
    PlayButton,
    HomeButton,
    UserButton,
    RemoveAdsButton,
    RankingButton,
    QuitButton, 
    BackButton,
    AcceptButton,
    CancelButton
}

[Serializable]
public class UIButtonController
{
    [SerializeField] private Button button;
    [SerializeField] private UIButtonType buttonType;

    public Button GetButtonByType(UIButtonType _buttonType)
    {
        if (buttonType == _buttonType)
        {
            return button;
        }
        return null;
    }
}
