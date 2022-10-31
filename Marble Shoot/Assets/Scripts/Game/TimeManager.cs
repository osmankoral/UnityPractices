using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Text timeText;
    private int time;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        time = 90;
        timeText.text = time.ToString();
        InvokeRepeating("Timer", 1f, 1f);
    }

    private void Timer()
    {
        if(time <= 0)
        {
            gameManager.EndGame();
        }
        time--;
        timeText.text = time.ToString();
    }

}
