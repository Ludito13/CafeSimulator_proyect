using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coffee_SO", menuName = "ScriptableObjects/Coffee_List")]
public class SO_Coffee : ScriptableObject
{
    public string coffeeName;
    public int id;
    public Sprite coffeeImage;
}
