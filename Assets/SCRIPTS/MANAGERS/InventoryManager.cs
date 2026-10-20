using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public List<ItemInventory> allItems = new List<ItemInventory>();
    public InventoryCanvas inventoryCanvas;
    public int maxCapacity;

    public Transform[] itemsPos;

    void Start()
    {
        allItems = new List<ItemInventory>(maxCapacity);

        EventManager.Suscribe<ItemInventory>(1, AddToInventory);
        EventManager.Suscribe<ItemInventory, Transform>(2, RestToInventory);
    }

    public void AddToInventory(ItemInventory item)
    {
        var freeSlot = itemsPos.FirstOrDefault(e => e.childCount == 0);

        if (freeSlot == null) return;

        allItems.Add(item);

        item.transform.SetParent(freeSlot);
        item.transform.position = freeSlot.position;

        inventoryCanvas.AddItemToCanvas(item);

        if (itemsPos.Any(x => x.childCount >= 1)) Debug.Log("Esta lleno");

    }

    public void RestToInventory(ItemInventory item, Transform repos)
    {
        if (!allItems.Contains(item))
        {
            Debug.Log("El ítem no está en el inventario");
            return;
        }

        inventoryCanvas.RestItemToCanvas(item);
        allItems.Remove(item);
        item.transform.SetParent(repos);
        item.transform.position = repos.position;
    }
}
