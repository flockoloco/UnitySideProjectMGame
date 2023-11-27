using Newtonsoft.Json;
using System;
using Unity.Services.Leaderboards;
using UnityEngine;

public class ScoreScript : AuthScript
{

    private async void AddNewScore()
    {
        int score = GameStatsManager._instance.changeScore(0);
        if (string.IsNullOrWhiteSpace(score.ToString()))
        {
            return;
        }

        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score);
            Debug.Log(JsonConvert.SerializeObject(playerEntry));
            //messageText.text = "Score submitted!";
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to submit score: {e}");
            throw;
        }
    }

    private async void GetScore()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }
     
}
