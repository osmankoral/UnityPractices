using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemDatabaseScriptableObject itemDb;
    [SerializeField] private GameObject itemSquarePref;
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform currentItemPanel;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemLevelText;
    [SerializeField] private Text itemClassText;
    [SerializeField] private Text itemAttackNoText;
    [SerializeField] private Text itemRangeNoText;

    private GameObject[] items = new GameObject[20];

    ItemManager itemManager;

    private int currentItemIndex;

    void Awake()
    {
        itemManager = GameObject.FindObjectOfType<ItemManager>();
    }

    void Start()
    {
        
         for(int i = 0; i < items.Length; i++)
        {
            GameObject item = Instantiate(itemSquarePref, inventoryPanel);
            item.GetComponent<Button>().onClick.AddListener(() => ButtonPress());
            items[i] = item;

        }
    }
    void Update()
    {
        for(int i = 0; i< itemDb.itemsYouHave.Count;i++)
        {
            items[i].gameObject.GetComponent<Image>().sprite = itemDb.itemsYouHave[i].itemSprite;
        }
        for(int i = itemDb.itemsYouHave.Count; i< items.Length; i++)
        {
            items[i].gameObject.GetComponent<Image>().sprite = itemSquarePref.GetComponent<Image>().sprite;
        }
        CurrentItemValues();
    }

    private void ButtonPress()
    {
        currentItemPanel.GetChild(0).GetComponent<Image>().sprite = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite; 
        
        for(int i=0; i<items.Length; i++)
        {
            if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == items[i])
            {
                currentItemIndex = i;
                break;
            }
        }
                CurrentItemValues();
        
    }

    public void ItemDelete()
    {
        itemManager.ItemRemove(currentItemIndex);
    }

    public void FirstWeaponEquipment()
    {
        itemManager.FistWeapon(currentItemIndex);
    }

    public void SecondWeaponEquipment()
    {
        itemManager.SecondWeapon(currentItemIndex);
    }

    public void ItemUpgrade()
    {
        itemManager.ItemUpgrade(currentItemIndex);
    }

    private void CurrentItemValues()
    {
        if(currentItemIndex > itemDb.itemsYouHave.Count)
            currentItemIndex = 0;
        itemName.text = itemDb.itemsYouHave[currentItemIndex].itemName;
        //if(itemDb.itemsYouHave[currentItemIndex].level > 0)
            //itemLevelText.text = "+" + itemDb.itemsYouHave[currentItemIndex].level.ToString();
        if((int)itemDb.itemsYouHave[currentItemIndex].itemClass == 1 )
            itemClassText.text = "Warrior";
        else if((int)itemDb.itemsYouHave[currentItemIndex].itemClass == 2)
            itemClassText.text = "Wizzard";
        itemAttackNoText.text = itemDb.itemsYouHave[currentItemIndex].attackDamage.ToString();
        itemRangeNoText.text = itemDb.itemsYouHave[currentItemIndex].attackRange.ToString();
    }
}
