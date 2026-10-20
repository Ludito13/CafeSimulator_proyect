using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoffeeMachine : MonoBehaviour
{
    public float timer = 5f;

    public CoffeeItem[] coffeeOptions;

    public InventoryManager inv;

    public CoffeeMachineUI coffeeMachineUI;

    public Transform[] spaces;
    public Transform[] readySpaces;

    private CoffeeSlot[] _slots;

    void Start()
    {
        _slots = spaces.Select(x => new CoffeeSlot(x)).ToArray();

        foreach (var item in coffeeOptions)
        {
            SO_Coffee data = item.coffeeData;
            item.button.onClick.AddListener(() => StartCoffee(data));
        }

        EventManager.Suscribe<ItemInventory>(6, ChooseCup);
    }

    public void ActiveCoffeeMachine()
    {
        if(!HasEmptySpace())
        {
            Debug.Log("Necesito tazas");
            return;
        }

        GameManager.instance.isSelectingCup = true;
        GameManager.instance.isCoffeOpen = true;
        inv.inventoryCanvas.OpenInventory();

    }

    public void DesactiveMachine()
    {
        
        GameManager.instance.isCoffeOpen = false;
    }

    public void ChooseCup(ItemInventory d)
    {
        CoffeeSlot slot = GetFreeSlot();

        if (!HasEmptySpace())
        {
            GameManager.instance.isSelectingCup = false;
            return;
        }

        if (!CheckSpace()) return;

        if (GetFreeSlot() == null)
        {
            Debug.Log("Necesitas liberar espacio");
            return;
        }

        Debug.Log($"{SpaceIndex()}");

        int i = Array.IndexOf(_slots, slot);

        slot.PlacedCup(d);
        inv.inventoryCanvas.CloseInventory();
        EventManager.Trigger(2, d, spaces[i]);

        Collider c = d.gameObject.GetComponent<Collider>();
        if (c != null) c.enabled = false;

        coffeeMachineUI.ActiveCoffeeUI();
        GameManager.instance.isSelectingCup = false;
    }

    void StartCoffee(SO_Coffee c)
    {
        GameManager.instance.isCoffeOpen = false;
        coffeeMachineUI.DesactiveCoffeeUI();

        CoffeeSlot slot = _slots.FirstOrDefault(s => s.WaitingForCoffee);
        if (slot == null) return;

        Debug.Log("Vamos a hacer el cafe");

        if (slot.StartBrew(c)) StartCoroutine(Brew(slot));
    }

    IEnumerator Brew(CoffeeSlot slot)
    {
        yield return new WaitForSeconds(timer);
        slot.FinishBrew();

        int i = Array.IndexOf(_slots, slot);

        slot.Cup.gameObject.transform.SetParent(readySpaces[i]);
        slot.Cup.gameObject.transform.position = readySpaces[i].position;
        slot.Cup.GetComponent<Collider>().enabled = true;
        Debug.Log("Listo: " + slot.Current.coffeeName);
        TakeCup(slot);
    }

    void TakeCup(CoffeeSlot slot)
    {
        InventoryItemInteract interact = slot.Cup.GetComponent<InventoryItemInteract>();

        if (interact != null)
        {
            UnityAction collect = null;
            collect = () =>
            {
                interact.events.RemoveListener(collect);
                slot.Take();
            };
            interact.events.AddListener(collect);
        }
        else
        {
            Debug.LogWarning("La taza no tiene InventoryItemIntera");
        }


    }

    public void CollectCup(ItemInventory cup)
    {
        CoffeeSlot slot = _slots.FirstOrDefault(s => s.Cup == cup);
        if (slot != null) slot.Take();
    }

    int SpaceIndex()
    {
        int index = 0;

        for (int i = 0; i <= spaces.Length - 1; i++)
        {
            if (spaces[i].childCount == 0) return i;

        }

        return index;
    }


    CoffeeSlot GetFreeSlot()
    {
        return _slots.FirstOrDefault(s => s.isFree);
    }

    bool CheckSpace() => spaces.Any(x => x.childCount <= 0);

    bool HasEmptySpace() => inv.allItems.Any(x => x.item.data.itemName == "Cup");
}
