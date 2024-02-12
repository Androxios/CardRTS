using Riptide;
using Riptide.Utils;
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

//This class manages network communication on the server.
public class NetworkManager : MonoBehaviour
{
    //Serialized field for the network settings scriptable object.
    [SerializeField] private NetworkSettingsSO m_netSettings;

    //Awake method.
    private void Awake()
    {
        //Initialize Riptide logger.
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);
    }

    //Reference to the server instance.
    public Server Server { get; private set; }

    //Start method.
    private void Start()
    {
        //Create a new server instance and start it.
        Server = new Server();
        Server.Start(m_netSettings.Port, m_netSettings.MaxPlayers);

        //Subscribe to network events.
        Subscribe();
    }

    //OnDestroy method.
    private void OnDestroy()
    {
        //Unsubscribe from network events.
        Unsubscribe();
    }

    //Subscribe to network events.
    private void Subscribe()
    {
        //Subscribe to events for sending messages to clients.
        NetworkEvents.SendMessageToPlayer -= SendToPlayer;
        NetworkEvents.SendMessageToAll += SendToAll;
    }

    //Method to send a message to all clients.
    private void SendToAll(Message msg)
    {
        Server.SendToAll(msg);
    }

    //Method to send a message to a specific client.
    private void SendToPlayer(Message msg, ushort id)
    {
        Server.Send(msg, id);
    }

    //Unsubscribe from network events.
    private void Unsubscribe()
    {
        //Unsubscribe from events for sending messages to clients.
        NetworkEvents.SendMessageToPlayer -= SendToPlayer;
        NetworkEvents.SendMessageToAll -= SendToAll;
    }

    //FixedUpdate method.
    private void FixedUpdate()
    {
        //Update the server.
        Server.Update();
    }
}