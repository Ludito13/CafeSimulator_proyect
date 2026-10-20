using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isInventoryOpen;
    public bool isCoffeOpen;
    public bool isSelectingCup;

    void Awake()
    {
        if(instance == null) instance = this;
    }
}
