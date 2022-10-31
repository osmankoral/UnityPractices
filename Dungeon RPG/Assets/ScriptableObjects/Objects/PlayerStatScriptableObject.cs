using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Objects/Player/PlayerStat")]
public class PlayerStatScriptableObject : ScriptableObject
{
    public float maxHealth;
    public float maxMana;
    public float maxStamina;
    public float attackDamage;
    public float attackSpeed;
    public float exp;
    public int level;
    public float maxExp;

    void Awake()
    {
        if(maxHealth == 0)
            maxHealth = 100;
        if(maxMana == 0)
            maxMana = 100;
        if(maxStamina == 0)
            maxStamina = 50;
        if(maxExp == 0)
            maxExp = 50;
        if(attackDamage == 0)
            attackDamage = 5;
        if(attackSpeed == 0)
            attackSpeed = 0.7f;
    }
    public void ExpGain(float _exp)
    {
        exp += _exp;
        if(exp >= maxExp)
        {
            level++;
            float _max = exp - maxExp;
            exp = 0 + _max;

            maxExp *= 2;
        }
    }

}
