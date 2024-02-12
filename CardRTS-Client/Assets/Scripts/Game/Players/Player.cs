using Riptide;
using UnityEngine;

// This class represents a player in the game.
public class Player : MonoBehaviour
{
    // Properties to store player ID, username, and whether the player is local
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    public bool IsLocal { get; private set; }

    // Method to initialize player with ID, username, and local status
    public void Init(ushort id, string username, bool isLocal)
    {
        Id = id;
        Username = username;
        IsLocal = isLocal;
    }

    // OnDestroy method
    private void OnDestroy()
    {
        // Remove player from player manager when destroyed
        PlayerManager.RemovePlayer(Id);
    }

    #region Messages

    /* ========== MESSAGE SENDING ==========*/
    public void RequestInit()
    {
        Message msg = Message.Create(MessageSendMode.Reliable, ClientToServerMsg.RequestLogin); //Create a message to send to the server.
        msg.AddString(Username); //Add player's username to the message.
        NetworkEvents.OnSendMessage(msg); //Send the message.
    }

    #endregion
}
