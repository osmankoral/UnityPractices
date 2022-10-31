using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Text burgerCostText;

    private bool isMenu;
    public int burgerCost;

    void Start()
    {
        burgerCost = 10;
        burgerCostText.text = burgerCost.ToString();

    }

    void OnMouseDown()
    {
        isMenu = !isMenu;
        menuPanel.SetActive(isMenu);
    }

    public void BurgerCostUp()
    {
        burgerCost++;
        if(burgerCost > 15)
        {
            burgerCost = 15;
        }
        burgerCostText.text = burgerCost.ToString();
    }

    public void BurgerCostDown()
    {
        burgerCost--;
        if(burgerCost < 5)
        {
            burgerCost = 5;
        }
        burgerCostText.text = burgerCost.ToString();
    }
}
