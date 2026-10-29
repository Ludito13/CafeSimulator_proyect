using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private Stack<Transform> allScreens = new Stack<Transform>();
    private HashSet<Transform> screenInStack = new HashSet<Transform>();

    private void Start()
    {
        EventManager.Suscribe<Transform>(3, OpenScreen);
        EventManager.Suscribe(4, CloseScreen);
        EventManager.Suscribe(5, CloseAllScreens);
    }

    private void OnDestroy()
    {
        EventManager.Unsuscribe<Transform>(3, OpenScreen);
        EventManager.Unsuscribe(4, CloseScreen);
        EventManager.Unsuscribe(5, CloseAllScreens);
    }

    public void OpenScreen(Transform t)
    {
        if (!allScreens.Contains(t))
        {
            allScreens.Push(t);
            screenInStack.Add(t);

            t.gameObject.SetActive(true);
        }
    }

    public void CloseScreen()
    {
        if(allScreens.Count > 0)
        {
            Transform screenToClose = allScreens.Pop();
            screenInStack.Remove(screenToClose);

            screenToClose.gameObject.SetActive(false);  
        }
    }

    public void CloseAllScreens()
    {
        while(allScreens.Count > 0)
        {
            Transform actual = allScreens.Pop();

            if(actual != null) actual.gameObject.SetActive(false);    
        }

        screenInStack.Clear();
    }

}
