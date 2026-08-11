using System.Collections.Generic;
using System.Transactions;
using UnityEngine;


public enum UIType
{
    NotSelected = 0,
    Build,
}

public class UIController : MonoSingleton<UIController>
{
    Dictionary<UIType, UserInterface> uiDictionary = new Dictionary<UIType, UserInterface>();
    
    public bool Subscribe(UIType type, UserInterface uiMono)
    {
        if (uiDictionary.ContainsKey(type))
        {
            return false;
        }

        uiDictionary.Add(type, uiMono);
        return true;
    }
    public bool IsInitialized(UIType type)
    {
        return uiDictionary.ContainsKey(type);
    }
    public UserInterface GetUserInterface(UIType type)
    {
        return uiDictionary[type];
    }
}
