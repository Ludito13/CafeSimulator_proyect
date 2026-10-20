using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceWithPlayer : MonoBehaviour
{
    public bool canInteract;
    public float distance;
    public Transform objToCompare;

    public void Update()
    {
        bool dis = Vector3.Distance(transform.position, objToCompare.position) <= distance;

        Debug.Log("Distancia es " + dis);
        
        canInteract = dis ? true : false;
    }
}
