using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineUI : MonoBehaviour
{
    public Transform coffeeMachineCanvas;

    public void ActiveCoffeeUI()
    {
        EventManager.Trigger(3, coffeeMachineCanvas);
        GameManager.instance.isCoffeOpen = true;
    }

    public void DesactiveCoffeeUI()
    {
        EventManager.Trigger(5);
    }
}
