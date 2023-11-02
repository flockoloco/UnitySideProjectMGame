using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject letter;
    [SerializeField]
    private Transform spawnerInitialPosition;

    private float spawnerTimer;
    private int lettersToFinishRun;

    

    private void Start()
    {
        //Default Time to spawn letter without interference of difficulty or time
        spawnerTimer = 2.2f;
        //Amount of letters to the level end, can change based on difficulty
        lettersToFinishRun = 10;
    }

    private void Update()
    {
        //Spawn letter if timer is 0 and there's still letters in the counter
        while(spawnerTimer <= 0f && lettersToFinishRun > 0)
        {
            Instantiate(letter, spawnerInitialPosition);
            spawnerTimer = 2.2f;
            lettersToFinishRun--;
        }
        //Change based on difficulty over time
        spawnerTimer -= Time.deltaTime;

    }
}
