using Riptide;
using UnityEngine;

// This class represents a player on the server.
public class Player : MonoBehaviour
{
    //Properties to store player ID and username.
    public ushort Id { get; private set; }
    public string Username { get; private set; }

    //Method to initialize player with ID and username.
    public void Init(ushort id, string username)
    {
        Id = id;
        Username = username;
    }

    //OnDestroy method.
    private void OnDestroy()
    {
        //Remove player from player manager when destroyed.
        PlayerManager.RemovePlayer(Id);
    }

    #region Messages

    /* ========== MESSAGE SENDING ==========*/
    public void ApproveLogin(bool approve)
    {
        // Create a message with the approval status
        Message msg = Message.Create(MessageSendMode.Reliable, ServerToClientMsg.ApproveLogin);
        msg.AddBool(approve);

        //Send the message to the client with the player's ID.
        NetworkEvents.Send(msg, Id);
    }

    #endregion
}