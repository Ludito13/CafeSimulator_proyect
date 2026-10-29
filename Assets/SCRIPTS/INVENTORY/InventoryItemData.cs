using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Data", menuName = "ScriptableObjects/Inventory Data List")]
public class InventoryItemData : ScriptableObject
{
    public string itemName;
    public int id;
    public Sprite itemImage;
}
