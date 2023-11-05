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

    //reference to the object with the stats
    private GameStatsManager _objStatsHolder;

    private void Start()
    {
        _objStatsHolder = FindFirstObjectByType<GameStatsManager>();
    }

    private void Update()
    {
        _hearts.text = _objStatsHolder._playerHearts.ToString();
        _textScore.text = _objStatsHolder._playerScore.ToString();
    }

}
