using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject letter;
    [SerializeField]
    private Transform spawnerInitialPosition;

    private float _spawnerTimer;
    private int _lettersToFinishRun;
    private GameStatsManager _objStatsHolder;

    [SerializeField]
    private GameObject[] level;

    private void Start()
    {
        //Default Time to spawn letter without interference of difficulty or time
        _spawnerTimer = 2.2f;
        //Amount of letters to the level end, can change based on difficulty
        _lettersToFinishRun = 10;
        _objStatsHolder = FindFirstObjectByType<GameStatsManager>();
    }

    private void Update()
    {
        //Spawn letter if timer is 0 and there's still letters in the counter
        while(_spawnerTimer <= 0f && _lettersToFinishRun > 0)
        {
            GameObject temp = Instantiate(letter, spawnerInitialPosition);
            _spawnerTimer = 2.2f;
            _lettersToFinishRun--;
        }
        //Change based on difficulty over time
        _spawnerTimer -= Time.deltaTime;

    }

    void nextPhase()
    {
        
    }
}

