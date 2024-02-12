using Riptide;
using UnityEngine.Events;

//This static class defines events for network communication.
public static class NetworkEvents
{
    //Event for sending a message to the client & server.
    public static event UnityAction<Message> SendMessageToAll;

    //Method to send a message to the client & server.
    public static void Send(Message msg) => SendMessageToAll?.Invoke(msg);

    //Event for sending a message to client.
    public static event UnityAction<Message, ushort> SendMessageToPlayer;

    //Method to send a message to client.
    public static void Send(Message msg, ushort id) => SendMessageToPlayer?.Invoke(msg, id);
}