using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PhysicalSkillDatabase", menuName = "Objects/Player/PhysicalSkillDatabase")]
public class PhysicalSkillDatabase : ScriptableObject
{
    public int walkCount;
    public int jumpCount;
    public int dashCount;
    public int attackCount;
    public int walkLevel;
    public int jumpLevel;
    public int dashLevel;
    public int attackLevel;
    public int maxWalk;
    public int maxJump;
    public int maxDash;
    public int maxAttack;

    void Awake()
    {
        if(!PlayerPrefs.HasKey("MaxWalk"))
        {
            maxWalk = 100;
            PlayerPrefs.SetInt("MaxWalk", maxWalk);
        }
        if(!PlayerPrefs.HasKey("MaxJump"))
        {
            maxJump = 10;
            PlayerPrefs.SetInt("MaxJump", maxJump);
        }
        if(!PlayerPrefs.HasKey("MaxDash"))
        {
            maxDash = 10;
            PlayerPrefs.SetInt("MaxDash", maxDash);
        }
        if(!PlayerPrefs.HasKey("MaxAttack"))
        {
            maxAttack = 10;
            PlayerPrefs.SetInt("MaxAttack", maxAttack);
        }

        //////
        walkCount = PlayerPrefs.GetInt("WalkCount");
        jumpCount = PlayerPrefs.GetInt("JumpCount");
        dashCount = PlayerPrefs.GetInt("DashCount");
        attackCount = PlayerPrefs.GetInt("AttackCount");
        walkLevel = PlayerPrefs.GetInt("WalkLevel");
        jumpLevel = PlayerPrefs.GetInt("JumpLevel");
        dashLevel = PlayerPrefs.GetInt("DashLevel");
        attackLevel = PlayerPrefs.GetInt("AttackLevel");
        maxWalk = PlayerPrefs.GetInt("MaxWalk");
        maxJump = PlayerPrefs.GetInt("MaxJump");
        maxDash = PlayerPrefs.GetInt("MaxDash");
        maxAttack = PlayerPrefs.GetInt("MaxAttack");
    }

    public void WalkGain()
    {
        walkCount++; PlayerPrefs.SetInt("WalkCount", walkCount);

        if(walkCount >= maxWalk)
        {
            walkLevel++; PlayerPrefs.SetInt("WalkLevel", walkLevel);
            maxWalk *= 2; PlayerPrefs.SetInt("MaxWalk", maxWalk);
        }
    }

    public void JumpGain()
    {
        jumpCount++; PlayerPrefs.SetInt("JumpCount", jumpCount);
        if(jumpCount >= maxJump)
        {
            jumpLevel++; PlayerPrefs.SetInt("JumpLevel", jumpLevel);
            maxJump *= 2; PlayerPrefs.SetInt("MaxJump", maxJump);
        }
    }
        public void DashGain()
    {
        dashCount++; PlayerPrefs.SetInt("DashCount", dashCount);
        if(dashCount >= maxDash)
        {
            dashLevel++; PlayerPrefs.SetInt("DashLevel", dashLevel);
            maxDash *= 2;PlayerPrefs.SetInt("MaxDash", maxDash);
        }
    }
        public void AttackGain()
    {
        attackCount++; PlayerPrefs.SetInt("AttackCount", attackCount);
        if(attackCount >= maxAttack)
        {
            attackLevel++; PlayerPrefs.SetInt("AttackLevel", attackLevel);
            maxAttack *= 2; PlayerPrefs.SetInt("MaxAttack", maxAttack);
        }
    }

}
