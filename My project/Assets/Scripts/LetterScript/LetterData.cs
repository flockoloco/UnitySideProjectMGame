using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterData : MonoBehaviour
{
    private Rigidbody2D rb2;
    [SerializeField]
    private readonly string[] letter = new string[] { "A", "S", "D" };
    private bool isInsideCollider = false;


    public string letterValue { get; private set; }

    private string randomGen()
    {
        int r = Random.Range(0, letter.Length);

        return letter[r];
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        letterValue = randomGen();
        GetComponentInChildren<TextMeshProUGUI>().text = letterValue;
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
        isInsideCollider = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInsideCollider = false;
        //Maybe change PRESSED IN SEQUENCE STRIKE system to 0 like in guitar hero?
    }

    private void CheckPressedKey(string inputString)
    {
        if(isInsideCollider)
        {
            if(inputString[0].ToString().ToUpper() == letterValue)
            {
                Destroy(gameObject);
                Debug.Log("nice");
            }
            //Do Stuff when correct letter pressed
            else
            {
                Debug.Log("Wrong key buddy");
                Destroy(gameObject);
            }
        }
    }


}
