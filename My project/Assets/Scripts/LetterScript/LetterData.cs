using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterData : MonoBehaviour
{
    private Rigidbody2D rb2;
    [SerializeField]
    private readonly char[] letter = new char[] { 'A', 'S', 'D' };

    public char letterValue { get; private set; }

    private char randomGen()
    {
        int r = Random.Range(0, letter.Length);

        return letter[r];
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        letterValue = randomGen();
    }

    private void FixedUpdate()
    {
        rb2.AddForce(transform.right * -5, ForceMode2D.Impulse);
    }
}
