using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHealtBar, playerExpBar;

    [SerializeField]
    private Text playerNameTxt, playerHealthTxt, playerLvlTxt;

    private float maxPlayerHealth;
    public float playerHealt;
    private string playerName;
    private float playerMaxDmg;
    private float playerMinDmg;
    private float playerDmg;
    private int playerLevel;
    public int playerShowLevel;
    private int playerExp;
    private int LevelMaxExp;
    public float playerDefence;


    private void Start()
    {
        //PlayerPrefs
        playerName = PlayerPrefs.GetString("playerName");

        playerLevel = PlayerPrefs.GetInt("playerLevel");
        playerShowLevel = playerLevel;

        playerExp = PlayerPrefs.GetInt("playerExp");
        LevelMaxExp = PlayerPrefs.GetInt("levelMaxExp");

        maxPlayerHealth = PlayerPrefs.GetFloat("playerHealth");
        playerMinDmg = PlayerPrefs.GetFloat("playerMinDamage");
        playerMaxDmg = PlayerPrefs.GetFloat("playerMaxDamage");
        playerDefence = PlayerPrefs.GetFloat("playerDefence");


        playerHealt = maxPlayerHealth;


        playerHealtBar.transform.GetComponent<Slider>().maxValue = maxPlayerHealth;
        playerHealtBar.transform.GetComponent<Slider>().value = playerHealt;

        playerExpBar.transform.GetComponent<Slider>().maxValue = LevelMaxExp;
        playerExpBar.transform.GetComponent<Slider>().value = playerExp;


        playerNameTxt.text = playerName;
        playerHealthTxt.text = playerHealt.ToString();

        playerLvlTxt.text = playerLevel.ToString();
    }

    public void PlayerTakeDamage(float damage)
    {
        playerHealt -= damage;
        playerHealtBar.transform.GetComponent<Slider>().value = playerHealt;
        playerHealthTxt.text = playerHealt.ToString();
    }

    public float PlayerDamage()
    {
        return Random.Range(playerMinDmg, playerMaxDmg + 1);
    }

    public void PlayerAtackMove()
    {
        StartCoroutine(PlayerAtackAnimation());
    }

    IEnumerator PlayerAtackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.DOLocalMoveX(8, .2f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(.4f);

        gameObject.transform.DOLocalMoveX(0, 1f).SetEase(Ease.OutBack);
    }

    public void PlayerTakeMove()
    {
        StartCoroutine(PlayerTakeAnimation());
    }

    IEnumerator PlayerTakeAnimation()
    {
        yield return new WaitForSeconds(.4f);
        gameObject.transform.DOLocalMoveX(-1, 1f);
        yield return new WaitForSeconds(.2f);
        gameObject.transform.DOLocalMoveX(0, 1f).SetEase(Ease.InBounce);
    }

    public void ExpGain(int exp)
    {
        playerExp += exp;
        if (playerExp >= LevelMaxExp)
        {
            LevelUp();
        }
        else
        {
            PlayerPrefs.SetInt("playerExp", playerExp);
            playerExpBar.transform.GetComponent<Slider>().value = playerExp;
        }

    }

    private void LevelUp()
    {


        int fazla = playerExp - LevelMaxExp;
        int eski = LevelMaxExp;
        playerExp = 0;
        playerExp += fazla;
        PlayerPrefs.SetInt("playerExp", playerExp);

        playerLevel++;
        PlayerPrefs.SetInt("playerLevel", playerLevel);
        playerLvlTxt.text = playerLevel.ToString();
        playerShowLevel = playerLevel;

        LevelMaxExp = eski + (playerLevel * 20);
        PlayerPrefs.SetInt("levelMaxExp", LevelMaxExp);


        playerMinDmg += 2;
        PlayerPrefs.SetFloat("playerMinDamage", playerMinDmg);
        playerMaxDmg += 2;
        PlayerPrefs.SetFloat("playerMaxDamage", playerMaxDmg);
        maxPlayerHealth += 20;
        PlayerPrefs.SetFloat("playerHealth", maxPlayerHealth);

        // Stat Text and Slider
        playerHealt = maxPlayerHealth;
        playerHealtBar.transform.GetComponent<Slider>().maxValue = maxPlayerHealth;
        playerHealtBar.transform.GetComponent<Slider>().value = playerHealt;
        playerHealthTxt.text = playerHealt.ToString();

        playerExpBar.transform.GetComponent<Slider>().maxValue = LevelMaxExp;
        playerExpBar.transform.GetComponent<Slider>().value = playerExp;

    }
}
