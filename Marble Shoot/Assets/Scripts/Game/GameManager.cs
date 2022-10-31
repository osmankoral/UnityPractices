using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform areaPoint;
    [SerializeField] private GameObject[] playerPrefs;
    [SerializeField] private GameObject[] enemyPref;
    [SerializeField] private GameObject powerBar;
    [SerializeField] private Text pointText;
    [SerializeField] private Text winStrikeText;
    [SerializeField] private Text ClaimText;


    private Transform enemySide;
    private GameObject marble;

    public int index;
    public int claim = 1;
    public int point;
    public int winStrike;
    public Quaternion dif;

    private int gameSet;

    void Awake()
    {
        PlayerPrefs.SetInt("Point", point);
        enemySide = areaPoint.transform.GetChild(0).transform;
    }
    void Start()
    {
        CreateMarble();
        StartCoroutine(CreateEnemy(0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(marble != null)
            startPoint.transform.rotation = dif;
        if(claim <= 0)
        {
            EndGame();
        }

        if(index <= 0)
        {
            StartCoroutine(CreateEnemy(2.2f));
        }

        pointText.text = point.ToString();
        ClaimText.text = claim.ToString();

        if(winStrike >= 2)
        {
            winStrikeText.text = "x " + winStrike.ToString();
            winStrikeText.fontSize = 40 + (winStrike * 2);
        }else
        {
            winStrikeText.text = "";
        }

    }


    private void CreateMarble()
    {
        marble = Instantiate(playerPrefs[Random.Range(0, playerPrefs.Length)], startPoint.position, startPoint.rotation);
    }

    IEnumerator CreateEnemy(float delay)
    {
        gameSet++;
        if(gameSet >= 2)
        {
           Vector3 a = areaPoint.position;
           a.z += (0.50f * gameSet);
           areaPoint.position = a;
           
        }
        
        switch(gameSet)
        {
            case 0:
                index = 3;
                break;
            
            case 1:
                index = 3;
                break;
            case 2:
                index = 5;
                break;
            case 3:
                index = 5;
                break;
            
            case 4:
                index = 7;
                break;
            case 5:
                index = 7;
                break;
            default:
                index = enemySide.childCount;
                break;

                
        }

        yield return new WaitForSeconds(delay);
        for(int i=0; i<index; i++)
        {
            GameObject enemy = Instantiate(enemyPref[Random.Range(0, enemyPref.Length)], enemySide.GetChild(i).position, enemySide.GetChild(i).rotation);
        }
    }

    public void ReCreate()
    {
        CreateMarble();
    }

    public void PowerBarStart()
    {
        powerBar.SetActive(true);
        powerBar.GetComponent<Slider>().maxValue = 10;
        powerBar.GetComponent<Slider>().value = 0;
    }

    public void PowerBarValue(float value)
    {
        powerBar.GetComponent<Slider>().value = value;
    }
    public void PowerBarEnd()
    {
        powerBar.SetActive(false);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }
}
