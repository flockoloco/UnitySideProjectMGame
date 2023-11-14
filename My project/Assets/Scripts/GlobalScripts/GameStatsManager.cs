using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatsManager : MonoBehaviour
{
    public int _playerHearts { get; private set; } = 5;
    public int _playerScore { get; private set; } = 0;
    //1 being normal / 2 Hard
    public int _playerDifficulty { get; private set; } = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int changeHearts(int value)
    {
        _playerHearts = _playerHearts + value;
        return _playerHearts;
    }

    public int changeScore(int value)
    {
        _playerScore = _playerScore + value;
        return _playerScore;
    }
    public void changeDifficulty(int value)
    {
        // 1 to normal or 2 to hard
        _playerDifficulty = value;
    }
}
