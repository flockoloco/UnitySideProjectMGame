using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterData : MonoBehaviour
{
    private Rigidbody2D rb2;
    [SerializeField]
    private readonly string[] _letter = new string[] { "A", "S", "D" };
    private bool _isInsideCollider = false;

    //reference to the object with the stats
    private GameStatsManager objStatsHolder;

    public string letterValue { get; private set; }

    private string randomGen()
    {
        int r = Random.Range(0, _letter.Length);

        return _letter[r];
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        letterValue = randomGen();
        GetComponentInChildren<TextMeshProUGUI>().text = letterValue;
        objStatsHolder = FindFirstObjectByType<GameStatsManager>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            CheckPressedKey(Input.inputString);
        }
    }

    private void FixedUpdate()
    {
        rb2.AddForce(transform.right * -5, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isInsideCollider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInsideCollider = false;
        //Maybe change PRESSED IN SEQUENCE STRIKE system to 0 like in guitar hero?
    }

    private void CheckPressedKey(string inputString)
    {
        if(_isInsideCollider)
        {
            //Do Stuff when correct letter pressed
            if (inputString[0].ToString().ToUpper() == letterValue)
            {
                Destroy(gameObject);
                objStatsHolder.changeScore(1);
                Debug.Log("nice");
            }
            //Do Stuff ONLY when wrong letter pressed
            else
            {
                Debug.Log("Wrong key buddy");

                if(objStatsHolder.changeHearts(-1) == 0)
                {
                    //GAMEOVER;
                    //SAVE SCORE AND MOVE TO MAIN MENU
                    Debug.Log("GAME OVER");
                }

                Destroy(gameObject);
            }
        }
    }


}
