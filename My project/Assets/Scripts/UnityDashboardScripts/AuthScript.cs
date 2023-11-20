using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class AuthScript : MonoBehaviour
{
    protected const string leaderboardID = "LeaderboardGameProject";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
    }
    private async void Start()
    {
        AuthenticationService.Instance.SignedIn += OnSignedIn;
        AuthenticationService.Instance.SignInFailed += OnSignInFailed;

        Debug.Log("Signing in...");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void OnSignedIn()
    {
        Debug.Log($"Signed in as: {AuthenticationService.Instance.PlayerId}");
    }
    private void OnSignInFailed(RequestFailedException exception)
    {
        Debug.Log($"Sign in failed with exception: {exception}");
    }
}
