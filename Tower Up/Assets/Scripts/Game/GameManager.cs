using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject animationCard;
    [SerializeField] private Transform cardPos;
    [SerializeField] private Transform animationCardPos;
    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Toggle animaSkipToggle;
    [SerializeField] private Toggle AutoToggle;
    [SerializeField] private GameObject animationPanel;


    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text IneractiveTxt, turnNumTxt, turnText, stageNumText, stageText;
    private bool isTurnTextActive;

    [SerializeField] private Button atackBtn;
    [SerializeField] private Button defenceBtn;

    private GameObject enemy;

    PlayerManager playerManager;
    EnemyManagaer enemyManagaer;
    Enemy enemySt;
    BattleManager battleManager;

    private float playerDamage;
    private float enemyDamage;
    private int turnNumber = 1;
    private int enemyLevel;
    public int mainStage;
    private int stageNum;

    private bool isAuto;

    private bool isEnemyDead = false;
    private bool isPanel = true;
    private int currentCard;
    private float cardDamage;
    private float cardDefence;
    private float playerDefence;

    private void Awake()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        enemyManagaer = GameObject.FindObjectOfType<EnemyManagaer>();
        enemySt = GameObject.FindObjectOfType<Enemy>();
        battleManager = GameObject.FindObjectOfType<BattleManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainStage = PlayerPrefs.GetInt("CurrentStage");
        EnemyCreate();
        turnNumTxt.text = turnNumber.ToString();
        // battleManager.CardCombine();

    }

    // Update is called once per frame
    void Update()
    {
        if (AutoToggle.isOn && isAuto && enemy != null)
            PlayerMove("Attack");
    }

    public void PlayerMove(string _move)
    {
        CardAnimation(_move);
    }

    private void PlayerAtack()
    {
        isAuto = false;
        IneractiveTxt.text = "Player Turn!";

        turnNumber++;
        turnNumTxt.text = turnNumber.ToString();

        playerManager.PlayerAtackMove();
        enemyManagaer.EnemyTakeMove();

        cardDamage = battleManager.cards[currentCard].attackDamage;
        playerDamage = playerManager.PlayerDamage() * cardDamage;

        enemyManagaer.TakeDamage(playerDamage);

        if (enemyManagaer.enemyHealth <= 0)
        {
            isEnemyDead = true;
        }

        IneractiveTxt.text = "Player is " + " Gobline  to " + playerDamage.ToString() + " take damage!";

        if (isEnemyDead)
        {
            atackBtn.interactable = false;
            defenceBtn.interactable = false;
            IneractiveTxt.text = "Enemy is Dead!";
            TurnDeactive();
            playerManager.ExpGain(enemyManagaer.exp);


            Destroy(enemy);
            Invoke("EnemyCreate", 1f);
            atackBtn.interactable = true;
            defenceBtn.interactable = true;

        }
        else
        {
            atackBtn.interactable = false;
            defenceBtn.interactable = false;
            StartCoroutine(EnemyAtack());
        }



    }

    IEnumerator EnemyAtack()
    {
        IneractiveTxt.text = "Enemy Turn!";

        yield return new WaitForSeconds(1f);

        enemyManagaer.EnemyAtackMove();
        playerManager.PlayerTakeMove();

        enemyDamage = enemyManagaer.EnemyDamage();
        enemyDamage -= (playerManager.playerDefence / 10);
        if (enemyDamage < 0)
        {
            enemyDamage = 0;
        }

        playerManager.PlayerTakeDamage(enemyDamage);

        IneractiveTxt.text = "Enemy is " + " Player  to " + enemyDamage.ToString() + " take damage!";

        if (playerManager.playerHealt <= 0)
        {
            IneractiveTxt.text = "Player is Dead";
            atackBtn.interactable = false;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);

            atackBtn.interactable = true;
            IneractiveTxt.text = "Player Turn!";
            isAuto = true;
        }


    }

    private void EnemyCreate()
    {
        StageManager();
        isEnemyDead = false;

        enemy = Instantiate(enemyPrefabs[mainStage - 1], enemyPosition);

        IneractiveTxt.text = "Player Turn!";
        atackBtn.interactable = true;
        TurnActive();
        isAuto = true;
    }

    public void PanelOn()
    {
        panel.SetActive(isPanel);

        isPanel = !isPanel;
    }
    public void BackLobby()
    {
        SceneManager.LoadScene("LobbyLevelScene");
    }

    IEnumerator CreateCards(string _move)
    {
        int newValue = currentCard - 10;
        if(newValue < 0)
            newValue = 100 - newValue;
        
        
        for (int i =0;  i<14; i++)
        {
            newValue = newValue%100;
            // 11nd end time
            GameObject a = Instantiate(animationCard, animationCardPos);
            if(_move == "Attack")
                a.transform.GetChild(0).GetComponent<Text>().text = battleManager.cards[newValue].attackDamage.ToString();
            
            if(_move == "Defence")
                a.transform.GetChild(0).GetComponent<Text>().text = battleManager.cards[newValue].defencePoint.ToString();

            newValue++;
            yield return new WaitForSeconds(0.4f);
        }

    }
    private void CardAnimation(string _move)
    {
        if(_move == "Attack")
            battleManager.AttackCardCombine();
        if(_move == "Defence")
            battleManager.DefenceCardCombine();
        currentCard = Random.Range(0, 100);


        if (!animaSkipToggle.isOn)
        {
            // Card Drop Animation
            animationPanel.SetActive(true);
            StartCoroutine(CreateCards(_move));

            StartCoroutine(AnimationDelay(_move));
        }
        else
        {
            GameObject newCard = Instantiate(card, cardPos);
            if(_move == "Attack")
                newCard.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = battleManager.cards[currentCard].attackDamage.ToString();

             if(_move == "Defence")
                newCard.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = battleManager.cards[currentCard].defencePoint.ToString();

            StartCoroutine(CardDelay(newCard));
            if(_move == "Attack")
                PlayerAtack();
            else if(_move == "Defence")
                PlayerDefence();
        }

    }


    IEnumerator AnimationDelay(string _move)
    {
        yield return new WaitForSeconds(5f);
        animationPanel.SetActive(false);

        GameObject newCard = Instantiate(card, cardPos);
        newCard.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = battleManager.cards[currentCard].attackDamage.ToString();

        StartCoroutine(CardDelay(newCard));

        if(_move == "Attack")
            PlayerAtack();
        else if(_move == "Defence")
            PlayerDefence();
    }

    IEnumerator CardDelay(GameObject _a)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(_a);
    }


    private void TurnActive()
    {
        isTurnTextActive = !isTurnTextActive;
        turnText.gameObject.SetActive(isTurnTextActive);
        turnNumTxt.gameObject.SetActive(isTurnTextActive);
        turnNumTxt.text = turnNumber.ToString();
    }

    private void TurnDeactive()
    {
        isTurnTextActive = !isTurnTextActive;
        turnText.gameObject.SetActive(isTurnTextActive);
        turnNumTxt.gameObject.SetActive(isTurnTextActive);
        turnNumber = 1;

    }

    private void StageManager()
    {
        if (!PlayerPrefs.HasKey("MainStage"))
        {
            mainStage = 0;
            PlayerPrefs.SetInt("MainStage", mainStage);
        }

        mainStage++;
        if (PlayerPrefs.GetInt("MainStage") < mainStage)
            PlayerPrefs.SetInt("MainStage", mainStage);


        stageNumText.text = mainStage.ToString();
    }

    private void PlayerDefence()
    {
        cardDefence = battleManager.cards[currentCard].defencePoint;
        playerDefence = playerManager.playerDefence * cardDefence;

        atackBtn.interactable = false;
        StartCoroutine(EnemyAtack());
    }
}
