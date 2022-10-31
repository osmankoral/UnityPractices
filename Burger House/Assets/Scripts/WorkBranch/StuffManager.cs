using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    [SerializeField] private GameObject botBreadPref;
    [SerializeField] private GameObject topBreadPref;
    [SerializeField] private GameObject meatPref;
    [SerializeField] private GameObject domatoPref;
    [SerializeField] private GameObject onionPref;
    [SerializeField] private Transform instantePos;
    [SerializeField] private Transform burger;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "Case1":
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                break;

            case "Case2":
                Domato();
                break;

            case "Case3":
                Meat();
                break;

            case "Case4":
                Onion();
                break;
            case "a1":
                BotBread();
                gameObject.transform.parent.gameObject.SetActive(false);
                break;
            case "a2":
                TopBread();
                gameObject.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    private void BotBread()
    {
        Instantiate(botBreadPref, instantePos.position, instantePos.rotation, burger);
        gameManager.breadCount++;
        gameManager.burger.Add("BotBread");
        gameManager.money -= 1;
        PlayerPrefs.SetInt("Money", gameManager.money);
    }

    private void TopBread()
    {
        Instantiate(topBreadPref, instantePos.position, instantePos.rotation, burger);
        gameManager.breadCount++;
        gameManager.burger.Add("TopBread");
        gameManager.money -= 1;
        PlayerPrefs.SetInt("Money", gameManager.money);
    }

    private void Meat()
    {
        Instantiate(meatPref, instantePos.position, instantePos.rotation, burger);
        gameManager.meatCount++;
        gameManager.burger.Add("Meat");
        gameManager.money -= 2;
        PlayerPrefs.SetInt("Money", gameManager.money);
    }

    private void Domato()
    {
        Instantiate(domatoPref, instantePos.position, instantePos.rotation, burger);
        gameManager.domatoCount++;
        gameManager.burger.Add("Domato");
        gameManager.money -= 1;
        PlayerPrefs.SetInt("Money", gameManager.money);
    }

    private void Onion()
    {
        Instantiate(onionPref, instantePos.position, instantePos.rotation, burger);
        gameManager.onionCount++;
        gameManager.burger.Add("Onion");
    }
}
