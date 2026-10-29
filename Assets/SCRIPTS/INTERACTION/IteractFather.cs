using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class IteractFather : MonoBehaviour
{
    public DistanceWithPlayer disWithPlayer;
    [Space(10)]
    public UnityEvent events;
    
    public virtual void OnMouseEnter()
    {
        if(disWithPlayer.canInteract) gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        else gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public virtual void  OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public virtual void OnMouseDown()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;

        events?.Invoke();

    }
}
