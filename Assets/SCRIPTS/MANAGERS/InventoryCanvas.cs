using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public Transform parent;
    public ItemUI itemPrefab;

    private List<ItemUI> allItemUI = new List<ItemUI>();

    public void OpenInventory()
    {
        inventoryCanvas.SetActive(true);
    }
    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
    }

    public void AddItemToCanvas(ItemInventory iv)
    {
        var newObj = Instantiate(itemPrefab, parent);
        newObj.SetItem(iv);
        allItemUI.Add(newObj);
    }

    public void RestItemToCanvas(ItemInventory iv)
    {
        var i = allItemUI.Find(x => x.inv_item.data.id == iv.item.data.id);

        if (i == null) return;

        allItemUI.Remove(i);
        Destroy(i.gameObject);

    }
}
