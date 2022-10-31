using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Objects/Items/WeaponItem")]
public class WeaponItemObject : ItemScriptableObject
{
    public int level;
    public float attackDamage;
    public float attackRange;
    public bool isRanged;

    public float level1AttackDamage;
    public float level2AttackDamage;
    public float level3AttackDamage;

    public void Awake()
    {
        itemType = ItemType.Weapon;
    }
    public void Upgrade()
    {
        level++;
        switch(level)
        {
            case 1:
                Level1();
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
        }
    }

    private void Level1()
    {
        attackDamage = level1AttackDamage;
    }

    private void Level2()
    {
        attackDamage = level2AttackDamage;
    }
    private void Level3()
    {
        attackDamage = level3AttackDamage;
    }
}
