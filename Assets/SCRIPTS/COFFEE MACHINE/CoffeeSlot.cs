using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotState
{
    Empty,
    CupPlaced,
    Brewing,
    Ready
}

public class CoffeeSlot
{
    public Transform anchor { get; private set; }
    public SlotState State { get; private set; }
    public SO_Coffee Current { get; private set; }
    public ItemInventory Cup { get; private set; }

    public event Action<CoffeeSlot, SlotState, SlotState> StateChanged;

    public CoffeeSlot(Transform c)
    {
        this.anchor = c;
    }
    
    public bool isFree => State == SlotState.Empty;
    public bool WaitingForCoffee => State == SlotState.CupPlaced;

    public bool PlacedCup(ItemInventory c)
    {
        if(State != SlotState.Empty) return false;
        Cup = c;
        Change(SlotState.CupPlaced);
        return true;
    }

    public bool StartBrew(SO_Coffee coffee)
    {
        if (State != SlotState.CupPlaced) return false;
        Current = coffee;
        Debug.Log("Empecemos el cafe");
        Change(SlotState.Brewing);
        return true;
    }

    public bool FinishBrew()
    {
        if (State != SlotState.Brewing) return false;
        Change(SlotState.Ready);
        return true;
    }

    public SO_Coffee Take()
    {
        if (State != SlotState.Ready) return null;
        Debug.Log("El cafe esta listo");
        SO_Coffee result = Current;
        Current = null;
        Change(SlotState.Empty);
        return result;
    }

    void Change(SlotState next)
    {
        SlotState prev = State;
        State = next;
        StateChanged?.Invoke(this, prev, next);
    }
}
