using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillDatabase", menuName = "Objects/Player/SkillDatabase")]
public class SkillDatabaseObject : ScriptableObject
{
    // Class Set DataBase maybe int array change to list
    public int[] warriorSkillSet = new int[4];
    public int[] wizardSkillSet = new int[4];

    // Warrior Class
    public int warriorClassLevel;
    public int warriorClassExp;
    public int warriorClassMaxExp;

    // Wizard Class
    public int wizzardClassLevel;
    public int wizardClassExp;
    public int wizardClassMaxExp;

            // Skill Level
    // Fireball
    public int fireballLevel;
    public int fireballExp;
    public int fireballMaxExp;

    //Iceball
    public int iceballLevel;
    public int iceballExp;
    public int iceballMaxExp;


    // Functions
    void Awake()
    {
        if(warriorClassMaxExp == 0)
            warriorClassMaxExp = 20;
        if(wizardClassMaxExp == 0)
            wizardClassMaxExp = 20;
        if(fireballMaxExp == 0)
            fireballMaxExp = 10;
        if(iceballMaxExp == 0)
            iceballMaxExp = 10;
    }

    public void WarriorClassGain()
    {
        warriorClassExp++;
        if(warriorClassExp >= warriorClassMaxExp)
        {
            warriorClassLevel++;
            warriorClassMaxExp *= 2;
        }
    }

    public void WizardClassGain()
    {
        wizardClassExp++;
        if(wizardClassExp >= warriorClassMaxExp)
        {
            wizzardClassLevel++;
            warriorClassMaxExp *= 2;
        }
    }

        public void FireballGain()
    {
        fireballExp++;
        if(fireballExp >= fireballMaxExp)
        {
            fireballLevel++;
            fireballMaxExp *= 2;
        }
    }

        public void IceballGain()
    {
        iceballExp++;
        if(iceballExp >= iceballMaxExp)
        {
            iceballLevel++;
            iceballMaxExp *= 2;
        }
    }
}
