using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Objects/Items/ItemDatabase")]
public class ItemDatabaseScriptableObject : ScriptableObject
{
    public WeaponItemObject firstWeapon;
    public WeaponItemObject secondtWeapon;
    public List<WeaponItemObject> itemsYouHave = new List<WeaponItemObject>();

    public void ItemAdd(WeaponItemObject _item)
    {
        itemsYouHave.Add(_item);
    }

    public void ItemRemove(int _index)
    {
        itemsYouHave.Remove(itemsYouHave[_index]);
    }
    
}
