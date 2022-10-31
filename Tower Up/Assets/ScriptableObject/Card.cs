using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    attack,
    defence,
    attackSpell,
    defenceSpell
}
[CreateAssetMenu(menuName = "Cards")]
public class Card : ScriptableObject
{
    public Type type;
    public float attackDamage;
    public float defencePoint;
}
