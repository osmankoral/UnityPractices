using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Text playerLevelTxt, playerExpTxt, playerHealthTxt, playerNameText, playerDamageTxt, playerDefenceTxt,
    atackPointTxt, defencePointTxt, healthPointTxt, pointText;

    [SerializeField]
    private Button atackPointBtn, defencePointBtn, healthPointBtn;
    [SerializeField] private Button stage11, stage21, stage1, playButton;

    private int point, atpo, depo, hepo, lvl, LevelMaxExp, eski;
    private float playerMinDmg, playerMaxDmg, maxPlayerHealth;
    [SerializeField] private InputField level;

    // Start is called before the first frame update
    void Start()
    {
        playButton.gameObject.SetActive(true);
        stage1.gameObject.SetActive(false);
        stage11.gameObject.SetActive(false);
        stage21.gameObject.SetActive(false);
        PlayerStatLoad();
    }
    public void ReLevel()
    {
        PlayerPrefs.SetInt("playerLevel", int.Parse(level.text));
        for(int i=0; i<PlayerPrefs.GetInt("playerLevel"); i++)
        {
            eski = PlayerPrefs.GetInt("levelMaxExp");
            LevelMaxExp = eski + (PlayerPrefs.GetInt("playerLevel") * 20);
            PlayerPrefs.SetInt("levelMaxExp", LevelMaxExp);

            playerMinDmg += 2;
            PlayerPrefs.SetFloat("playerMinDamage", playerMinDmg);
            playerMaxDmg += 2;
            PlayerPrefs.SetFloat("playerMaxDamage", playerMaxDmg);
            maxPlayerHealth += 20;
            PlayerPrefs.SetFloat("playerHealth", maxPlayerHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        atpo = PlayerPrefs.GetInt("atackPoint");
        depo = PlayerPrefs.GetInt("defencePoint");
        hepo = PlayerPrefs.GetInt("healthPoint");
        lvl = PlayerPrefs.GetInt("playerLevel");

        point = (lvl * 3) - (atpo + depo + hepo);
        pointText.text = point.ToString();

        if (point > 0)
        {
            atackPointBtn.gameObject.SetActive(true);
            defencePointBtn.gameObject.SetActive(true);
            healthPointBtn.gameObject.SetActive(true);
        }else
        {
            atackPointBtn.gameObject.SetActive(false);
            defencePointBtn.gameObject.SetActive(false);
            healthPointBtn.gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        if(PlayerPrefs.GetInt("MainStage") < 10)
        {
            SceneManager.LoadScene("GameLevelScene");
            PlayerPrefs.SetInt("CurrentStage", 0);
        }

        else if(PlayerPrefs.GetInt("MainStage") >= 10 && PlayerPrefs.GetInt("MainStage") < 20)
            {
                playButton.gameObject.SetActive(false);
                stage1.gameObject.SetActive(true);
                stage11.gameObject.SetActive(true);
            }

        else if(PlayerPrefs.GetInt("MainStage") >= 20 && PlayerPrefs.GetInt("MainStage") < 30)    
            {
                playButton.gameObject.SetActive(false);
                stage1.gameObject.SetActive(true);
                stage11.gameObject.SetActive(true);
                stage21.gameObject.SetActive(true);
            }
        
    }

    public void selectedStage1()
    {
        PlayerPrefs.SetInt("CurrentStage", 0);
        SceneManager.LoadScene("GameLevelScene");
    }

    public void selectedStage11()
    {
        PlayerPrefs.SetInt("CurrentStage", 10);
        SceneManager.LoadScene("GameLevelScene");
    }

        public void selectedStage21()
    {
        PlayerPrefs.SetInt("CurrentStage", 20);
        SceneManager.LoadScene("GameLevelScene");
    }

    private void PlayerStatLoad()
    {
        //Player Level Prefs
        if(!PlayerPrefs.HasKey("playerLevel"))
        {
            PlayerPrefs.SetInt("playerLevel", 1);
        }


        //Player Exp Prefs
        if(!PlayerPrefs.HasKey("playerExp"))
        {
            PlayerPrefs.SetInt("playerExp", 0);
        }


        //Max Exp Pref
        if(!PlayerPrefs.HasKey("levelMaxExp"))
        {
            PlayerPrefs.SetInt("levelMaxExp", 20);
        }


        //Player Health
        if(!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 100f);
        }



        //Player Min Damage
        if(!PlayerPrefs.HasKey("playerMinDamage"))
        {
            PlayerPrefs.SetFloat("playerMinDamage", 1f);
        }

        //Plyer Max Damage
        if(!PlayerPrefs.HasKey("playerMaxDamage"))
        {
            PlayerPrefs.SetFloat("playerMaxDamage", 10f);
        }


        //Player Defence
        if(!PlayerPrefs.HasKey("playerDefence"))
        {
            PlayerPrefs.SetFloat("playerDefence", 10f);
        }

        //Atack Point
        if(!PlayerPrefs.HasKey("atackPoint"))
        {
            PlayerPrefs.SetInt("atackPoint", 1);
        }

        //Defence Point
        if (!PlayerPrefs.HasKey("defencePoint"))
        {
            PlayerPrefs.SetInt("defencePoint", 1);
        }

        //Health Point
        if (!PlayerPrefs.HasKey("healthPoint"))
        {
            PlayerPrefs.SetInt("healthPoint", 1);
        }

        atpo = PlayerPrefs.GetInt("atackPoint");
        depo = PlayerPrefs.GetInt("defencePoint");
        hepo = PlayerPrefs.GetInt("healthPoint");
        lvl = PlayerPrefs.GetInt("playerLevel");

        point = (lvl * 3) - (atpo + depo + hepo);

        //Points
        PlayerPrefs.SetInt("point", point);
        
        playerMinDmg = PlayerPrefs.GetFloat("playerMinDamage"); playerMaxDmg = PlayerPrefs.GetFloat("playerMaxDamage"); maxPlayerHealth = PlayerPrefs.GetFloat("playerHealth");




        playerNameText.text = PlayerPrefs.GetString("playerName");
        playerLevelTxt.text = PlayerPrefs.GetInt("playerLevel").ToString();
        playerExpTxt.text = PlayerPrefs.GetInt("playerExp").ToString() + " / " + PlayerPrefs.GetInt("levelMaxExp").ToString();
        playerHealthTxt.text = PlayerPrefs.GetFloat("playerHealth").ToString();
        playerDamageTxt.text = PlayerPrefs.GetFloat("playerMinDamage").ToString() + " - " + PlayerPrefs.GetFloat("playerMaxDamage").ToString();
        playerDefenceTxt.text = PlayerPrefs.GetFloat("playerDefence").ToString();

        atackPointTxt.text = PlayerPrefs.GetInt("atackPoint").ToString();
        defencePointTxt.text = PlayerPrefs.GetInt("defencePoint").ToString();
        healthPointTxt.text = PlayerPrefs.GetInt("healthPoint").ToString();
        pointText.text = PlayerPrefs.GetInt("point").ToString();
        

    } 
    public void Rest()
        {
            PlayerPrefs.SetFloat("playerHealth",100f);
            PlayerPrefs.SetFloat("playerMinDamage",1f);
            PlayerPrefs.SetFloat("playerMaxDamage",10f);
            PlayerPrefs.SetInt("playerExp",0);
            PlayerPrefs.SetFloat("playerDefence",10f);
            PlayerPrefs.SetInt("levelMaxExp",20);
            PlayerPrefs.SetInt("playerLevel",1);

        PlayerPrefs.SetInt("atackPoint", 1);
        PlayerPrefs.SetInt("defencePoint", 1);
        PlayerPrefs.SetInt("healthPoint",1);
        PlayerPrefs.SetInt("MainStage",0);




        playerNameText.text = PlayerPrefs.GetString("playerName");
        playerLevelTxt.text = PlayerPrefs.GetInt("playerLevel").ToString();
        playerExpTxt.text = PlayerPrefs.GetInt("playerExp").ToString() + " / " + PlayerPrefs.GetInt("levelMaxExp").ToString();
        playerHealthTxt.text = PlayerPrefs.GetFloat("playerHealth").ToString();
        playerDamageTxt.text = PlayerPrefs.GetFloat("playerMinDamage").ToString() + " - " + PlayerPrefs.GetFloat("playerMaxDamage").ToString();
        playerDefenceTxt.text = PlayerPrefs.GetFloat("playerDefence").ToString();

        atackPointTxt.text = PlayerPrefs.GetInt("atackPoint").ToString();
        defencePointTxt.text = PlayerPrefs.GetInt("defencePoint").ToString();
        healthPointTxt.text = PlayerPrefs.GetInt("healthPoint").ToString();
        pointText.text = PlayerPrefs.GetInt("point").ToString();

    }

    public void AtackAdd()
    {
        atpo++;
        PlayerPrefs.SetInt("atackPoint", atpo);
        atackPointTxt.text = atpo.ToString();

        float min = PlayerPrefs.GetFloat("playerMinDamage");
        min += 5;
        PlayerPrefs.SetFloat("playerMinDamage", min);

        float max = PlayerPrefs.GetFloat("playerMaxDamage");
        max += 5;
        PlayerPrefs.SetFloat("playerMaxDamage", max);

        playerDamageTxt.text = PlayerPrefs.GetFloat("playerMinDamage").ToString() + " - " + PlayerPrefs.GetFloat("playerMaxDamage").ToString();
    }

    public void DefenceAdd()
    {
        depo++;
        PlayerPrefs.SetInt("defencePoint", depo);
        defencePointTxt.text = depo.ToString();

        float min = PlayerPrefs.GetFloat("playerDefence");
        min += 10;
        PlayerPrefs.SetFloat("playerDefence", min);

        
        playerDefenceTxt.text = PlayerPrefs.GetFloat("playerDefence").ToString();
    }

    public void HealthAdd()
    {
        hepo++;
        PlayerPrefs.SetInt("healthPoint", hepo);
        healthPointTxt.text = hepo.ToString();

        float min = PlayerPrefs.GetFloat("playerHealth");
        min += 50;
        PlayerPrefs.SetFloat("playerHealth", min);

        playerHealthTxt.text = PlayerPrefs.GetFloat("playerHealth").ToString();
    }
}
