using Riptide;
using System.Collections.Generic;
using UnityEngine;

//This class manages player instantiation and interactions on the server.
public class PlayerManager : MonoBehaviour
{
    //Serialized field for the player prefab.
    [SerializeField] private GameObject m_PlayerPrefab;

    //Static reference to the player prefab.
    private static GameObject s_PlayerPrefab;

    //Dictionary to store players by their ID.
    private static Dictionary<ushort, Player> s_Players = new Dictionary<ushort, Player>();

    //Static method to get a player by ID.
    public static Player GetPlayer(ushort id)
    {
        s_Players.TryGetValue(id, out Player player);
        return player;
    }

    //Static method to remove a player by ID.
    public static bool RemovePlayer(ushort id)
    {
        if (s_Players.TryGetValue(id, out Player player))
        {
            s_Players.Remove(id);
            return true;
        }
        return false;
    }

    //Awake method.
    private void Awake()
    {
        s_PlayerPrefab = m_PlayerPrefab;
    }

    //Method to spawn a player with the given ID and username.
    private static void SpawnPlayer(ushort id, string username)
    {
        //Instantiate player prefab at position (0, 0, 0) with no rotation.
        Player player = Instantiate(s_PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();

        //Set player name and initialize.
        player.name = $"{username} -- {id}";
        player.Init(id, username);

        //Add player to dictionary.
        s_Players.Add(id, player);

        //Simulate login approval (could be validation from a database).
        bool shouldApprove = true;
        player.ApproveLogin(shouldApprove);
    }

    #region Messages

    /* ========== MESSAGE RECEIVING ==========*/
    [MessageHandler((ushort)ClientToServerMsg.RequestLogin)]
    private static void ReceiveLoginRequest(ushort fromId, Message msg)
    {
        //Extract username from the message and spawn a player with the received ID and username.
        string username = msg.GetString();
        SpawnPlayer(fromId, username);
    }

    #endregion
}