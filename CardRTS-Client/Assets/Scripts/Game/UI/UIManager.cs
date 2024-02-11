using Androxios.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public LocalSceneUI LocalUI;

    public void Connect()
    {
        if(LocalUI == null)
        {
            Debug.LogError("No local UI on this scene.");
        }
        string connectInput = "ConnectInput";
        if(!LocalUI.Components.TryGetValue(connectInput, out UIComponent component))
        {
            Debug.LogError($"No input component found: {connectInput}");
        }
        InputComponent input = (InputComponent)component;
        string username = input.Input.text;
        NetworkManager.Instance.Connect(username);
    }
}