using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemDatabaseScriptableObject ItemDatabase;
    
    ItemScriptableObjectManager itemManager;

    public WeaponItemObject firstWeapon;
    public WeaponItemObject secondtWeapon;

    public float firstWeaponAttack;
    public float secondWeaponAttack;
    public float firstWeaponAttackRange;
    public float secondWeaponAttackRange;
    public bool firstWeaponisRanged;
    public bool secondWeaponisRanged;

    void Awake()
    {
        itemManager = GameObject.FindObjectOfType<ItemScriptableObjectManager>(); 
    }

    void Start()
    {
        firstWeapon = ItemDatabase.firstWeapon;
        secondtWeapon = ItemDatabase.secondtWeapon;

        firstWeaponAttack = firstWeapon.attackDamage;
        secondWeaponAttack = secondtWeapon.attackDamage;
        firstWeaponAttackRange = firstWeapon.attackRange;
        secondWeaponAttackRange = secondtWeapon.attackRange;
        firstWeaponisRanged = firstWeapon.isRanged;
        secondWeaponisRanged = secondtWeapon.isRanged;
    }

    void Update()
    {
        firstWeapon = ItemDatabase.firstWeapon;
        secondtWeapon = ItemDatabase.secondtWeapon;

        firstWeaponAttack = firstWeapon.attackDamage;
        secondWeaponAttack = secondtWeapon.attackDamage;
        firstWeaponAttackRange = firstWeapon.attackRange;
        secondWeaponAttackRange = secondtWeapon.attackRange;
        firstWeaponisRanged = firstWeapon.isRanged;
        secondWeaponisRanged = secondtWeapon.isRanged;
    }
    public void FistWeapon(int _index)
    {
        ItemDatabase.ItemAdd(ItemDatabase.firstWeapon);
        ItemDatabase.firstWeapon = ItemDatabase.itemsYouHave[_index];

        PlayerPrefs.SetInt("FirstWeapon", ItemDatabase.firstWeapon.itemID);
        PlayerPrefs.SetInt("FirstWeaponLevel", ItemDatabase.firstWeapon.level);

        

        ItemDatabase.ItemRemove(_index);
    }

    public void SecondWeapon(int _index)
    {
        ItemDatabase.ItemAdd(ItemDatabase.secondtWeapon);
        ItemDatabase.secondtWeapon = ItemDatabase.itemsYouHave[_index];
        PlayerPrefs.SetInt("SecondWeapon", ItemDatabase.secondtWeapon.itemID);
        PlayerPrefs.SetInt("SecondWeaponLevel", ItemDatabase.secondtWeapon.level);
        ItemDatabase.ItemRemove(_index);
    }

    public void ItemAdd()
    {
        if(ItemDatabase.itemsYouHave.Count < 20)
        {
            //ItemDatabase.ItemAdd(itemManager.items[Random.Range(0, itemManager.items.Length)]);
            ItemDatabase.ItemAdd(itemManager.items[Random.Range(0, itemManager.items.Length)]);

            itemManager.ADD();
        }  
    }

    public void ItemRemove(int _index)
    {
        ItemDatabase.ItemRemove(_index);
        itemManager.ADD();
    }

    public void ItemUpgrade(int _index)
    {
        Debug.Log(ItemDatabase.itemsYouHave[_index].attackDamage);
        ItemDatabase.itemsYouHave[_index].Upgrade();
        Debug.Log(ItemDatabase.itemsYouHave[_index].attackDamage);
    }

    public void ItemDropRate(float _rate)
    {
        int a = 100 - Random.Range(0,100);
        if(_rate > (float)a)
            ItemAdd();
    }
}
