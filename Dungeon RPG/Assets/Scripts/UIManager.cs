using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [SerializeField] private GameObject Ppanel;
    [SerializeField] private GameObject screenColorChange;



    // Scriptable Object
    [SerializeField] private PlayerStatScriptableObject playerStat;
    [SerializeField] private SkillDatabaseObject skillDb;

    // Scripts Referances
    CameraControl cameraControl;
    ItemManager itemManager;
    GameManager gameManager;

    //UI Object
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform characterPanel;
    [SerializeField] private Transform skillPanel;
    [SerializeField] private Transform skillBar;
    [SerializeField] private Slider healthBar; 
    [SerializeField] private Slider manaBar; 
    [SerializeField] private Slider staminaBar; 
    [SerializeField] private Slider expBar; 

    [SerializeField] private Text playerName;
    [SerializeField] private Text levelText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text manaText;
    [SerializeField] private Text staminaText;
    [SerializeField] private Text expText;


    
    private GameObject player;

    private bool isInventoryOn;
    private bool isCharacterOn;
    private bool isSkillOn;

    void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        cameraControl = GameObject.FindObjectOfType<CameraControl>();
        itemManager = GameObject.FindObjectOfType<ItemManager>();
    }
    void Start()
    {
        player = gameManager.player;
    }

    // Update is called once per frame
    void Update()
    {   

        if(player != null)
        {
            PlayerStatValues();
            SwitchClass();
            ScreenColorChange();
        }else // player object write a  in Game Manager Prefab; 
            player = gameManager.player;

        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryOn_Off();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            CharacterPanelOn_Off();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            SkillPanelOn_Off();
        }

        EquipmentCheck();

    }

    private void PlayerStatValues()
    {
        playerName.text = "PLAYER";
        levelText.text = player.GetComponent<PlayerManager>().level.ToString();
        healthText.text = player.GetComponent<PlayerManager>().health.ToString()+"/"+player.GetComponent<PlayerManager>().maxHealth.ToString();
        manaText.text = player.GetComponent<PlayerManager>().mana.ToString()+"/"+player.GetComponent<PlayerManager>().maxMana.ToString();
        staminaText.text = player.GetComponent<PlayerManager>().stamina.ToString()+"/"+player.GetComponent<PlayerManager>().maxStamina.ToString();
        float _exp = (player.GetComponent<PlayerManager>().exp * player.GetComponent<PlayerManager>().maxExp)/100;
        expText.text = "%"+_exp.ToString();

        healthBar.maxValue = player.GetComponent<PlayerManager>().maxHealth;
        healthBar.value = player.GetComponent<PlayerManager>().health;

        manaBar.maxValue = player.GetComponent<PlayerManager>().maxMana;
        manaBar.value = player.GetComponent<PlayerManager>().mana;
        
        staminaBar.maxValue = player.GetComponent<PlayerManager>().maxStamina;
        staminaBar.value = player.GetComponent<PlayerManager>().stamina;

        expBar.maxValue = player.GetComponent<PlayerManager>().maxExp;
        expBar.value = player.GetComponent<PlayerManager>().exp;
    }

    private void SwitchClass()
    {
        
        if(player.GetComponent<PlayerManager>().isFirstWeapon /*&& !player.GetComponent<PlayerManager>().isPotion*/)
        {
            skillBar.GetChild(0).gameObject.SetActive(true);
            skillBar.GetChild(1).gameObject.SetActive(false);
            skillBar.GetChild(2).gameObject.SetActive(false);
            skillBar.GetChild(0).GetComponent<Image>().sprite = itemManager.firstWeapon.itemSprite;
            if(player.GetComponent<PlayerManager>().classType == 1)
            {
                skillBar.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
            }else if(player.GetComponent<PlayerManager>().classType == 2)
            {
                skillBar.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Ppanel.transform.GetChild(1).GetComponent<Image>().sprite;
                skillBar.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Ppanel.transform.GetChild(2).GetComponent<Image>().sprite;
                skillBar.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
            }
        }
        if(!player.GetComponent<PlayerManager>().isFirstWeapon /*&& !player.GetComponent<PlayerManager>().isPotion*/)
        {
            skillBar.GetChild(0).gameObject.SetActive(false);
            skillBar.GetChild(1).gameObject.SetActive(true);
            skillBar.GetChild(2).gameObject.SetActive(false);
            skillBar.GetChild(1).GetComponent<Image>().sprite = itemManager.secondtWeapon.itemSprite;

            if(player.GetComponent<PlayerManager>().classType == 1)
            {
                skillBar.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(1).GetChild(2).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
            }else if(player.GetComponent<PlayerManager>().classType == 2)
            {
                skillBar.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Ppanel.transform.GetChild(1).GetComponent<Image>().sprite;
                skillBar.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Ppanel.transform.GetChild(2).GetComponent<Image>().sprite;
                skillBar.GetChild(1).GetChild(2).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
                skillBar.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Ppanel.transform.GetChild(0).GetComponent<Image>().sprite;
            }
        }
       /* if(player.GetComponent<PlayerManager>().isPotion)
        {
            skillBar.GetChild(0).gameObject.SetActive(false);
            skillBar.GetChild(1).gameObject.SetActive(false);
            skillBar.GetChild(2).gameObject.SetActive(true);
        }*/
    }
    
    

    public void InventoryOn_Off()
    {
        if(isSkillOn)
            SkillPanelOn_Off();
        
        isInventoryOn = !isInventoryOn;
        inventoryPanel.gameObject.SetActive(isInventoryOn);
    }

    public void CharacterPanelOn_Off()
    {
        isCharacterOn = !isCharacterOn;
        characterPanel.gameObject.SetActive(isCharacterOn);
        
        if(isCharacterOn && !isInventoryOn)
            InventoryOn_Off();
    }
    public void SkillPanelOn_Off()
    {
        if(isInventoryOn)
            InventoryOn_Off();

        isSkillOn = !isSkillOn;
        skillPanel.gameObject.SetActive(isSkillOn);
    }

    private void EquipmentCheck()
    {
        if(isCharacterOn)
        {
            characterPanel.GetChild(0).GetChild(1).GetComponent<Image>().sprite = itemManager.firstWeapon.itemSprite;
            characterPanel.GetChild(0).GetChild(2).GetComponent<Image>().sprite = itemManager.secondtWeapon.itemSprite;
        }
    }

    private void ScreenColorChange()
    {
        float alphaValue = (player.GetComponent<PlayerManager>().health * player.GetComponent<PlayerManager>().maxHealth)/ 100;
        if(alphaValue <=25)
        {
            screenColorChange.GetComponent<CanvasGroup>().alpha = 1 - (alphaValue / 25f);
        }
        else
        {
            screenColorChange.GetComponent<CanvasGroup>().alpha = 0; 

        }
    }

}
