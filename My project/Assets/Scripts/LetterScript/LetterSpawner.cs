using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject letter;
    [SerializeField]
    private Transform spawnerInitialPosition;

    private float _spawnerTimer = 1.0f;
    private int _lettersToFinishRun = 10;
    private GameStatsManager _objStatsHolder;

    [SerializeField]
    private GameObject[] levelsAvailable;
    private GameObject currentLevel;
    [SerializeField]
    private GameObject[] phases;
    private int currentPhase = 0;

    private bool _finishedAnimation = true;

    private void Start()
    {
        //Default Time to spawn letter without interference of difficulty or time
        //Amount of letters to the level end, can change based on difficulty
        _lettersToFinishRun = 10;
        _objStatsHolder = FindFirstObjectByType<GameStatsManager>();

        //Choose the level among the levels available and instantiate it
        currentLevel = levelsAvailable[Random.Range(0, levelsAvailable.Length)];
        GameObject level = Instantiate(currentLevel);
        //get current phases inside this same level
        phases = GameObject.FindGameObjectsWithTag("Phase");


        Camera.current.transform.position = phases[0].transform.position;
    }

    private void Update()
    {
        //Spawn letter if timer is 0 and there's still letters in the counter
        while (_spawnerTimer <= 0f && _lettersToFinishRun > 0 && _finishedAnimation)
        {
            GameObject temp = Instantiate(letter, spawnerInitialPosition);
            _spawnerTimer = 1f;
            _lettersToFinishRun--;

        }

        if (currentPhase >= phases.Length)
        {
            StartCoroutine(LoadAsyncScene("MainMenu"));
        }
        //Change based on difficulty over time
        _spawnerTimer -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        
        if (_lettersToFinishRun <= 0)
        {
            currentPhase += 1;
            _finishedAnimation = false;
  
            StartCoroutine(MoveCamera());
            _lettersToFinishRun = 10;

        }

    }

    IEnumerator MoveCamera()
    {
        
        Vector3 startPosition = Camera.main.transform.position;
        float speed = 1f / 3;
        for (float t = 0; t < 1; t += Time.deltaTime * speed)
        {
            Camera.main.transform.position = Vector3.Lerp(startPosition, new Vector3(phases[currentPhase].transform.localPosition.x, phases[currentPhase].transform.localPosition.y, -10), t);
            
            yield return new WaitForEndOfFrame();

        }
        _finishedAnimation = true;
        yield return null;
        
    }

    IEnumerator LoadAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

