using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monsters")]
public class Monster : ScriptableObject
{
    public string monsterName;
    public int monsterLevel;
    public int monsterHealth;
    public int monsterMaxAttack;
    public int monsterMinAttack;
    public int monsterExp;
    public int gold;
}
