using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class CloudServices : MonoBehaviour
{
    [SerializeField] private GameObject erroLoginPopup;

    public async void InitializeLoginConnectionVerification()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await SignInAnonymouslyAsync();
            UpdateUserNameUIInfos();
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
}
