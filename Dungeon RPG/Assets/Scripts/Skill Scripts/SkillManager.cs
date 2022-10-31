using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject fireballSkillPref;
    [SerializeField] private GameObject iceballSkillPref;

    [SerializeField] private SkillDatabaseObject SkillDb;

    PlayerManager playerManager;

    private float skillCooldown;
    private float skillCooldown1;
    private float skillCooldown2;
    private float skillCooldown3;
    private float skillCooldown4;
    private float skillCooldown5;
    private float skillCooldown6;
    private float skillCooldown7;
    private int cooldownNo;

    public List<float> skillCooldowns = new List<float>();
    


    void Awake()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }
    
    void Start()
    {
        skillCooldowns.Add(skillCooldown);
        skillCooldowns.Add(skillCooldown1);
        skillCooldowns.Add(skillCooldown2);
        skillCooldowns.Add(skillCooldown3);

        skillCooldowns.Add(skillCooldown4);
        skillCooldowns.Add(skillCooldown5);
        skillCooldowns.Add(skillCooldown6);
        skillCooldowns.Add(skillCooldown7);

    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();
    }
    public void Skill(int useSkillNo, bool isFirstWeapon, int _classType)
    {
        cooldownNo = useSkillNo;
        
        if(!isFirstWeapon)
            cooldownNo += 4;

        switch(_classType)
        {
            case 1:
                WarriorSkill(useSkillNo, cooldownNo);
                break;
            case 2:
                WizardSkill(useSkillNo, cooldownNo);
                break;
        }

    }

    private void WarriorSkill(int _skillNo, int _cooldown)
    {
        int skill = SkillDb.warriorSkillSet[_skillNo];
        switch(skill)
        {
            case 0:
                break;
        }
    }
    private void WizardSkill(int _skillNo, int _cooldown)
    {
        int skill = SkillDb.wizardSkillSet[_skillNo];
        
        switch(skill)
        {
            case 0:
                Fireball(_cooldown);
                break;
            case 1:
                Iceball(_cooldown);
                break;
        }
    }
    private void Cooldown()
    {
        if(skillCooldowns[0] >= 0)
        {
            skillCooldowns[0] -= Time.deltaTime;
        }
        if(skillCooldowns[1] >= 0)
        {
            skillCooldowns[1] -= Time.deltaTime;
        }
        if(skillCooldowns[2] >= 0)
        {
            skillCooldowns[2] -= Time.deltaTime;
        }
        if(skillCooldowns[3] >= 0)
        {
            skillCooldowns[3] -= Time.deltaTime;
        }
        if(skillCooldowns[4] >= 0)
        {
            skillCooldowns[4] -= Time.deltaTime;
        }
        if(skillCooldowns[5] >= 0)
        {
            skillCooldowns[5] -= Time.deltaTime;
        }
        if(skillCooldowns[6] >= 0)
        {
            skillCooldowns[6] -= Time.deltaTime;
        }
        if(skillCooldowns[7] >= 0)
        {
            skillCooldowns[7] -= Time.deltaTime;
        }
    }

    private void Fireball(int cooldown)
    {
        float mana = 10;

        if(playerManager.mana >= mana && skillCooldowns[cooldown] <= 0)
        {
            SkillDb.WizardClassGain();
            SkillDb.FireballGain();
            
            GameObject fireball = Instantiate(fireballSkillPref, transform.GetChild(0).position, transform.GetChild(0).rotation);
            skillCooldowns[cooldown] = 3f;
            playerManager.mana -= mana;
        }
        else if(playerManager.mana < mana)
        {
            Debug.Log("yetersiz Mana");
        }
        else if(skillCooldowns[cooldown] > 0)
        {
            Debug.Log("Bekleme Süresinde" + skillCooldowns[cooldown]);
            Debug.Log(skillCooldowns[cooldown]);
        }
        
    }

        private void Iceball(int cooldown)
    {
        float mana = 20;
        if(playerManager.mana >= mana && skillCooldowns[cooldown] <= 0)
        {
            SkillDb.WarriorClassGain();
            SkillDb.IceballGain();

            GameObject iceball = Instantiate(iceballSkillPref, transform.GetChild(0).position, transform.GetChild(0).rotation);
            skillCooldowns[cooldown] = 3f;
            playerManager.mana -= mana;
        }
        else if(playerManager.mana < mana)
        {
            Debug.Log("yetersiz Mana");
        }
        else if(skillCooldowns[cooldown] > 0)
        {
            Debug.Log("Bekleme Süresinde" + skillCooldowns[cooldown]);
            Debug.Log(skillCooldowns[cooldown]);
        }
        
    }

}
