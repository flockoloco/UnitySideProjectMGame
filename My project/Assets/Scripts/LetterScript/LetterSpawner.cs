using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterSpawner : MonoBehaviour
{
    //Object (prefab) letter to spawn
    [SerializeField]
    private GameObject letter;
    //List of letters still in game
    private static List<GameObject> _lettersAlive = new List<GameObject>();
    //Initial position which letters spawn
    [SerializeField]
    private Transform spawnerInitialPosition;
    // TODO: Change to reduce over time
    //Letters Spawn cooldown  
    private float _spawnerTimer = 1.0f;
    //Amount of letters to spawn each phase
    private int _lettersToFinishRun = 10;
    //Just some Quality of Life in the spawn of letters
    private float _timerToStartToSpawnLetters = 0.44f;

    //List of levelsAvailable only one level is chosen
    [SerializeField]
    private GameObject[] levelsAvailable;
    //List of phases inside the chosen level
    [SerializeField]
    private GameObject[] phases;
    //current phase between many
    private int currentPhase = 0;
    //Animation boolean that restrict spawn until camera moves to next phase
    private bool _animationIsPlaying = false;

    private void Start()
    {
        //Default Time to spawn letter without interference of difficulty or time
        //Amount of letters to the level end, can change based on difficulty
        _lettersToFinishRun = 10;

        //Choose a random level among the levels available and instantiate it
        GameObject level = Instantiate(levelsAvailable[Random.Range(0, levelsAvailable.Length)]);
        //get all phases inside instantiated level
        phases = GameObject.FindGameObjectsWithTag("Phase");
        //Set camera position to the first phase on level 
        Camera.current.transform.position = phases[0].transform.position;
    }

    

    private void Update()
    {
        //If currentPhase is after the last phase length then GG
        if (currentPhase >= phases.Length)
        {
            //Main MEnu has index 0
            MenuScripts.instance.LoadScene(0);
        }
        //Dont spawn until animation is finished
        if (!_animationIsPlaying && _timerToStartToSpawnLetters <= 0.0f)
        {
            //Spawn letter if timer is 0 and there's still letters in the counter
            if (_spawnerTimer <= 0.0f && _lettersToFinishRun > 0)
            {
                spawnLetters();
            }
        }
        //Change based on difficulty over time
        _spawnerTimer -= Time.deltaTime;

        //If there a 0 letters in the phase, move to next.
        if (_lettersToFinishRun <= 0 && _lettersAlive.Count <= 0)
        {
            ResetAndNextPhase();
        } //This else means that if there's letters to finish available but the timer is below 0 then we start the timer
          else { if (_timerToStartToSpawnLetters >= 0.0f) _timerToStartToSpawnLetters -= Time.deltaTime; }

    }
    //Reset/Decrease variables and spawn letter
    private void spawnLetters()
    {
        _spawnerTimer = 1f;
        GameObject temp = Instantiate(letter, spawnerInitialPosition);
        //add instantiated letter to the list
        _lettersAlive.Add(temp);
        
        _lettersToFinishRun--;
    }
    //reset variables and move camera
    private void ResetAndNextPhase()
    {
        //must be first, we are moving to next phase durrh
        currentPhase += 1;
        _animationIsPlaying = true;
        StartCoroutine(MoveCamera());
        //I think it doesnt matter this variable position because animationIsPlaying blocks anything to spawn
        _lettersToFinishRun = 10;
    }
    public static void removeThyFromList(GameObject item)
    {
        _lettersAlive.Remove(item);
    }
    IEnumerator MoveCamera()
    {
        
        Vector3 startPosition = Camera.main.transform.position;
        //over 3 seconds idk
        float speed = 1f / 3;
        //idk mate just a forloop 
        for (float t = 0; t < 1; t += Time.deltaTime * speed)
        {
            Camera.main.transform.position = Vector3.Lerp(startPosition, new Vector3(phases[currentPhase].transform.localPosition.x, phases[currentPhase].transform.localPosition.y, -10), t);
            //Wait for end of frame to move to next position... idk it works
            yield return new WaitForEndOfFrame();

        }
        _animationIsPlaying = false;
        //reset timer
        _timerToStartToSpawnLetters = 0.44f;
        yield return null;
    }

    
}

