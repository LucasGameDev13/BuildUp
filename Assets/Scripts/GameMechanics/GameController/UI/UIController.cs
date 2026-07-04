using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("UI Ranking Settings")]
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private RankingInfoToolTip rankingInfoToolTip;
    [SerializeField] private Transform rankingContainer;

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

        OpenScreen(hudPanel);
    }

    public void SetupGameOverScreen()
    {
        OpenScreen(gameOverPanel);
    }


    public void SetupScoreText(int _scoreValue)
    {
        scoreText.text = _scoreValue.ToString();
    }

    public async Task SetupRankingText(CloudServices cloudService)
    {
        foreach(Transform child in rankingContainer)
        {
            Destroy(child.gameObject);
        }

        List<PlayerRanking> players = await cloudService.GetScores();

        if (this == null || rankingInfoToolTip == null) return;

        foreach (PlayerRanking playersRanking in players)
        {
            RankingInfoToolTip newRankingInfo = Instantiate(rankingInfoToolTip, rankingContainer);
            newRankingInfo.InitializeTooltip(playersRanking.position + 1, playersRanking.userName, playersRanking.score);
        }
    }

    public void SetupHomeRecordScoreText(int score)
    {
        homeRecordScoreText.text = "My Record: - " + score.ToString();
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
        CloseCurrentPopup();
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
