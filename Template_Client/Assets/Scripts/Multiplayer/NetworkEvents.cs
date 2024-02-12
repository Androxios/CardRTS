using Riptide;
using UnityEngine.Events;

//This static class defines events for network communication.
public static class NetworkEvents
{
    //Event for a client's connection request to the server, with username and password parameters.
    public static event UnityAction<string, string> ConnectRequest;

    //Method to invoke the ConnectRequest event
    public static void OnConnectRequest(string username, string password) => ConnectRequest?.Invoke(username, password);

    //Event for a successful connection to the server, with client ID and username parameters
    public static event UnityAction<ushort, string> ConnectSuccess;

    //Method to invoke the ConnectSuccess event
    public static void OnConnectSuccess(ushort id, string username) => ConnectSuccess?.Invoke(id, username);

    //Event for sending a message to the server or other clients, with a Message parameter
    public static event UnityAction<Message> SendMessage;

    //Method to invoke the SendMessage event
    public static void OnSendMessage(Message msg) => SendMessage?.Invoke(msg);
}