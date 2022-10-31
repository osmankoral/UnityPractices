using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScriptableObjectManager : MonoBehaviour
{
    public WeaponItemObject[] items;
    [SerializeField] private ItemDatabaseScriptableObject ItemDatabase;

    private string slot = "Slot"; 
    private string itemLevel = "ItemLevel";
    private int slots;
    void Start()
    {
        
    }
    void Update()
    {
        if(ItemDatabase.itemsYouHave.Count != PlayerPrefs.GetInt("Slots"))
        {
            for(int i = 0; i < PlayerPrefs.GetInt("Slots"); i++)
            {
                string b = slot+i.ToString();
                string c = itemLevel+i.ToString();

                ItemDatabase.ItemAdd(items[PlayerPrefs.GetInt(b)]);
                ItemDatabase.itemsYouHave[i].level = PlayerPrefs.GetInt(c);
            }
            //ItemDatabase.itemsYouHave[i] = items[PlayerPrefs.GetInt(b)];
            //ItemDatabase.itemsYouHave[i].level = PlayerPrefs.GetInt(c);
        }

    }

    public void ADD()
    {
        slots = ItemDatabase.itemsYouHave.Count;
        PlayerPrefs.SetInt("Slots", slots);

        for(int i = 0; i<ItemDatabase.itemsYouHave.Count; i++)
        {
            string b = slot+i.ToString();
            PlayerPrefs.SetInt(b, ItemDatabase.itemsYouHave[i].itemID);
            string c = itemLevel+i.ToString();
            PlayerPrefs.SetInt(c, ItemDatabase.itemsYouHave[i].level);
        }
    }
}
