using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Item", menuName = "Objects/Items/Item")]
public enum ItemType
{
    Weapon,
    Equipment,
    Default
}

public enum ClassType
{
    None,
    Warrior,
    Wizard
}
public abstract class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite itemSprite;
    public ItemType itemType;
    public ClassType itemClass;
}
