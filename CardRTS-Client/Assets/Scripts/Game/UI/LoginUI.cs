using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This class manages the login UI functionality.
public class LoginUI : MonoBehaviour
{
    //Serialized fields for the username, password, and login button.
    [SerializeField] private TMP_InputField m_Username;
    [SerializeField] private TMP_InputField m_Password;
    [SerializeField] private Button m_LoginButton;

    //Method called when the login button is clicked.
    public void Connect()
    {
        //Get the username and password entered by the user.
        string username = m_Username.text;
        string password = m_Password.text;

        //Send a connection request with the provided username and password.
        NetworkEvents.OnConnectRequest(username, password);
    }
}