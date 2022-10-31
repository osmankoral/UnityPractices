using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] private Text highScore;
    [SerializeField] private Text score;
    void Start()
    {
        if(PlayerPrefs.GetInt("Point") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Point"));
        }

        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        score.text = PlayerPrefs.GetInt("Point").ToString();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

}
