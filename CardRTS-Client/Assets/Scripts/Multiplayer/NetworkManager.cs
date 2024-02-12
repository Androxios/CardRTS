using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;

//Enum defining message types from server to client.
public enum ServerToClientMsg : ushort
{
    ApproveLogin,
}

//Enum defining message types from client to server.
public enum ClientToServerMsg : ushort
{
    RequestLogin,
}

//This class manages network communication.
public class NetworkManager : MonoBehaviour
{
    //Awake method.
    protected void Awake()
    {
        //Initialize Riptide logger.
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);
    }

    //Reference to network settings scriptable object.
    [SerializeField] private NetworkSettingsSO m_netSettings;

    //Reference to the client instance.
    public Client Client { get; private set; }

    //Start method.
    private void Start()
    {
        Client = new Client(); //Create a new client instance.
        Client.Connected += OnClientConnected; //Subscribe to client connected event.
        Subscribe(); //Subscribe to network events.
    }

    //Subscribe to network events.
    private void Subscribe()
    {
        NetworkEvents.ConnectRequest += Connect;
        NetworkEvents.SendMessage += OnSendMessage;
    }

    //Unsubscribe from network events.
    private void Unsubscribe()
    {
        NetworkEvents.ConnectRequest -= Connect;
        NetworkEvents.SendMessage -= OnSendMessage;
    }

    //Method to send a message via the client.
    private void OnSendMessage(Message msg)
    {
        Client.Send(msg);
    }

    //Event handler for client connected event.
    private void OnClientConnected(object sender, EventArgs e)
    {
        //Invoke connect success event with client ID and local username.
        NetworkEvents.OnConnectSuccess(Client.Id, m_netSettings.LocalUsername);
        m_netSettings.LocalId = Client.Id;
    }

    //Method to connect to the server with username and password.
    public void Connect(string username, string password)
    {
        //Set local username (default to "Guest" if empty).
        m_netSettings.LocalUsername = string.IsNullOrEmpty(username) ? $"Guest" : username;

        //Connect to the server.
        Client.Connect($"{m_netSettings.Ip}:{m_netSettings.Port}");
    }

    //FixedUpdate method.
    private void FixedUpdate()
    {
        Client.Update(); //Update the client.
    }

    //OnDestroy method.
    protected void OnDestroy()
    {
        Unsubscribe(); //Unsubscribe from network events.
        Client.Connected -= OnClientConnected; //Unsubscribe from client connected event.
    }
}
