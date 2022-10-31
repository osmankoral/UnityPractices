using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] PlayerStatScriptableObject statDatabase;

    ItemManager itemManager;
    PlayerManager playerManager;

    private float attack1;
    private float attack2;
    private float attackRange1;
    private float attackRange2;
    private bool isRanged1;
    private bool isRanged2;
    private int classType1;
    private int classType2;
    private float maxHealth;
    private float maxMana;
    private float maxStamina;
    private float exp;
    private float maxExp;
    private int level;

    void Awake()
    {
        itemManager = GameObject.FindObjectOfType<ItemManager>();
    }
    
    void Start()
    {
        Stat();
    }

    void Update()
    {
        Stat();
        if(playerManager == null)
            playerManager = GameObject.FindObjectOfType<PlayerManager>();

        if(playerManager != null)
        {
            playerManager.StatUpdate(attack1, attack2, attackRange1, attackRange2, isRanged1, isRanged2, maxHealth, maxMana, maxStamina,
             classType1, classType2, exp, maxExp, level);
        }
    }
    private void Stat()
    {
        
        attack1 = statDatabase.attackDamage + itemManager.firstWeaponAttack;
        attack2 = statDatabase.attackDamage + itemManager.secondWeaponAttack;
        attackRange1 = itemManager.firstWeaponAttackRange;
        attackRange2 = itemManager.secondWeaponAttackRange;
        isRanged1 = itemManager.firstWeaponisRanged;
        isRanged2 = itemManager.secondWeaponisRanged;
        classType1 = (int)itemManager.firstWeapon.itemClass;
        classType2 = (int)itemManager.secondtWeapon.itemClass;
        exp = statDatabase.exp;
        maxExp = statDatabase.maxExp;
        level = statDatabase.level;

        maxHealth = statDatabase.maxHealth;
        maxMana = statDatabase.maxMana;
        maxStamina = statDatabase.maxStamina;
    }
}
