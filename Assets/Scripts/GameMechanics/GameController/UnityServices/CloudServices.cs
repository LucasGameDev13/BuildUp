using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class CloudServices : MonoBehaviour
{
    [SerializeField] private GameObject erroLoginPopup;
    [SerializeField] private string scoreTableLeaderboardID = "RankingScore";


    public async void InitializeLoginConnectionVerification()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await SignInAnonymouslyAsync();
            UpdateUserNameUIInfos();
            GameController.Instance.UIController.SetupHomeRecordScoreText(await GetPlayerScore());
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private async Task SignInAnonymouslyAsync()
    {
        if(AuthenticationService.Instance.IsSignedIn)
        {
            return;
        }

        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if(AuthenticationService.Instance.PlayerName == "" || AuthenticationService.Instance.PlayerName == null)
            {
                await UpdateUserName("Player");
            }
        }
        catch
        {
            erroLoginPopup.SetActive(true);
        }
    }

    public async void TryLoginConnectionAgain()
    {
        erroLoginPopup.SetActive(false);
        await SignInAnonymouslyAsync();
    }

    private async Task UpdateUserName(string userName)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(userName);
    }

    private void UpdateUserNameUIInfos()
    {
        string name = AuthenticationService.Instance.PlayerName;
        GameController.Instance.UIController.SetupUserNameText(name.Substring(0, name.IndexOf("#")));
        GameController.Instance.UIController.SetupUserNameInputField(name.Substring(0, name.IndexOf("#")));
    }

    public async void UpdateUserNameFromInput(TMP_InputField newName)
    {
        await UpdateUserName(newName.text);
        UpdateUserNameUIInfos();
    }






    public async void SaveScore(int score)
    {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(scoreTableLeaderboardID, score);
    }

    public async Task<List<PlayerRanking>> GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(scoreTableLeaderboardID);
        
        List<PlayerRanking> playerRankings = new List<PlayerRanking>();

        foreach (LeaderboardEntry entry in scoresResponse.Results)
        {
            PlayerRanking ranking = new PlayerRanking();

            ranking.position = entry.Rank;
            ranking.userName = entry.PlayerName.Substring(0, entry.PlayerName.IndexOf("#"));
            ranking.score = (int)entry.Score;

            playerRankings.Add(ranking);
        }

        return playerRankings;
    }
    
    private async Task<int> GetPlayerScore()
    {
        try
        {
            var result = await LeaderboardsService.Instance.GetPlayerScoreAsync(scoreTableLeaderboardID);
            return (int) result.Score;  
        }
        catch
        {
            return 0;
        }
    }

    [ContextMenu("Force New Player (TEST ONLY)")]
    public void ForceNewPlayer()
    {
        AuthenticationService.Instance.ClearSessionToken();
        AuthenticationService.Instance.SignOut();
        Debug.Log("Token limpo! Reinicie o jogo para gerar novo Player ID.");
    }

}


[Serializable]
public class PlayerRanking
{
    public int position;
    public string userName;
    public int score;
}

