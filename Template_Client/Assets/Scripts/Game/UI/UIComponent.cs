using System;
using TMPro;
using UnityEngine.UI;

//Base class for UI components.
public class UIComponent
{
    public string Key; //Key to identify the UI component.
}

//Serializable class for button UI components, inheriting from UIComponent.
[Serializable]
public class ButtonComponent : UIComponent
{
    public Button Button; //Reference to the Button component.
}

//Serializable class for input field UI components, inheriting from UIComponent.
[Serializable]
public class InputComponent : UIComponent
{
    public TMP_InputField Input; //Reference to the TMP_InputField component.
}