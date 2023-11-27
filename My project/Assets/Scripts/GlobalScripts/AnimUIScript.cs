using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimUIScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textScore;
    [SerializeField]
    private TextMeshProUGUI _hearts;

    private void Start()
    {
    
    }

    private void Update()
    {
        _hearts.text = GameStatsManager._instance._playerHearts.ToString();
        _textScore.text = GameStatsManager._instance._playerScore.ToString();
    }

}
