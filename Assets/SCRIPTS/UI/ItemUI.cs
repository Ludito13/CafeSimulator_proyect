using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Button itemButton;
    public TMP_Text itemText;
    public InventoryItem inv_item { get; private set; }
    private int _count;
    private ItemInventory itemInv;

    public void Start()
    {
        itemButton.onClick.AddListener(() => Action());
    }

    public ItemUI SetItem(ItemInventory i)
    {
        itemInv = i;
        inv_item = i.item;
        
        itemText.text = i.item.data.itemName;

        return this;
    }

    void Action()
    {
        if(GameManager.instance.isSelectingCup)
        {
            Debug.Log("Select Cup");
            EventManager.Trigger(6, itemInv);
        }
    }
}
