using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform customerPos;
    [SerializeField] private Button orderTakebutton;
    [SerializeField] private Button orderbutton;
    [SerializeField] private Text goldText;
    [SerializeField] private Text dayText;
    [SerializeField] private Text endDayMoneyText;
    [SerializeField] private Text endDayGainText;
    [SerializeField] private GameObject endDayPanel;

    private GameObject customer;
    StuffManager stuffManager;
    LevelManager levelManager;
    BurgerMenuManager burgerMenuManager;

    public int money;
    public int yesterDayMoney;
    private int day;
    private int dailyCustomer;

    public List<string> burger = new List<string>();
    public int meatCount;
    public int breadCount;
    public int domatoCount;
    public int onionCount;
    private bool isOnion;

    private int customerChat;

    private void GameStart()
    {
        // Value Set
        if (!PlayerPrefs.HasKey("Money"))
            PlayerPrefs.SetInt("Money", 50);



        // Value Get
        money = PlayerPrefs.GetInt("Money");
        day = PlayerPrefs.GetInt("Day");
        dailyCustomer = PlayerPrefs.GetInt("DailyCustomer");
        yesterDayMoney = PlayerPrefs.GetInt("YesterDayMoney");
    }
    void Awake()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        stuffManager = GameObject.FindObjectOfType<StuffManager>();
        burgerMenuManager = GameObject.FindObjectOfType<BurgerMenuManager>();
    }

    void Start()
    {
        GameStart();
        goldText.text = money.ToString();
        levelManager.Level1On();
        meatCount = 0;
        breadCount = 0;
        domatoCount = 0;

        Invoke("Customer", 2f);
    }

    void Update()
    {
        if (dailyCustomer <= 0)
        {
            DayEnd();
        }
    }

    public void Level1()
    {
        levelManager.Level1On();
        BurgerCheck();
        CustomerChat();

    }

    private void level2()
    {
        levelManager.Level2On();
        meatCount = 0;
        breadCount = 0;
        domatoCount = 0;
        onionCount = 0;

        if (burger != null)
        {
            burger.RemoveRange(0, burger.Count);
        }
    }

    public void BurgerTakeOrder()
    {
        orderTakebutton.gameObject.SetActive(false);
        orderbutton.gameObject.SetActive(true);
        level2();
    }

    public void BurgerOrder()
    {
        dailyCustomer--;
        PlayerPrefs.SetInt("DailyCustomer", dailyCustomer);

        orderTakebutton.gameObject.SetActive(false);
        orderbutton.gameObject.SetActive(false);
        // New Customer and update
        Destroy(customer, 1f);
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        Customer();
    }

    private void Customer()
    {
        customer = Instantiate(customerPrefab, customerPos);
        int a = Random.Range(1, 3);
        if (a == 1)
        {
            isOnion = true;
            customerChat = 5;
            CustomerChat();
        }
        else
        {
            isOnion = false;
            customerChat = 6;
            CustomerChat();
        }

        orderTakebutton.gameObject.SetActive(true);
        orderbutton.gameObject.SetActive(false);
    }

    private void DayEnd()
    {
        int b = money - yesterDayMoney;
        endDayPanel.SetActive(true);
        endDayMoneyText.text = money.ToString();
        endDayGainText.text = b.ToString();
    }

    public void NewDay()
    {
        endDayPanel.SetActive(false);

        day++;
        PlayerPrefs.SetInt("Day", day);
        dayText.text = day.ToString();

        yesterDayMoney = money;
        PlayerPrefs.SetInt("YesterDayMoney", yesterDayMoney);
        if (burgerMenuManager != null)
        {
            dailyCustomer = 5 - (burgerMenuManager.burgerCost / 5);
            PlayerPrefs.SetInt("DailyCustomer", dailyCustomer);
        }

    }

    private void BurgerCheck()
    {
        if (burger[0] == "BotBread" && burger[burger.Count - 1] == "TopBread")
        {
            customerChat = 1;
            money += burgerMenuManager.burgerCost;
            goldText.text = money.ToString();
            if (meatCount > 1)
            {
                customerChat = 3;
                int a = Random.Range(1, 3);
                if (a == 1)
                    money += meatCount * 5;
                goldText.text = money.ToString();
            }
            if (!isOnion)
            {
                if (onionCount > 0)
                {
                    customerChat = 4;
                    int a = (onionCount * 2);
                    if (a > burgerMenuManager.burgerCost)
                        a = 0;
                    money -= a;
                    goldText.text = money.ToString();
                }
            }
        }
        else
        {
            customerChat = 2;
            money += 0;
            goldText.text = money.ToString();
        }
        PlayerPrefs.SetInt("Money", money);
    }

    private void CustomerChat()
    {
        switch (customerChat)
        {
            case 1:
                // İyi
                customer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Hamburger Çok İyi";
                break;
            case 2:
                // Çok Kötü
                customer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Buna Hamburger mi Diyorsun?";
                break;
            case 3:
                // Çok İyi Eti Bol
                customer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Hamburger Çok İyi Eti Bol";
                break;
            case 4:
                // Soğanlı bu KÖtü
                customer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Hamburger Kötü Soğğanlı";
                break;
            case 5:
                // Hamburger
                customer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Hamburger Alabilir miyim?";
                break;
            case 6:
                // Soğansız Hamburger
                customer.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Soğansız Hamburger Alabilir miyim?";
                break;
        }
    }
}