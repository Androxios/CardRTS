using Riptide;
using System.Collections.Generic;
using UnityEngine;

//This class manages player instantiation and interactions in the game.
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private NetworkSettingsSO m_netSettings; //Reference to network settings scriptable object.
    [SerializeField] private GameObject m_PlayerPrefab; //Prefab for the player object.
    private static GameObject s_PlayerPrefab; //Static reference to the player prefab.
    private static Dictionary<ushort, Player> s_Players = new Dictionary<ushort, Player>(); //Dictionary to store players by ID.

    //Static method to get a player by ID.
    public static Player GetPlayer(ushort id)
    {
        s_Players.TryGetValue(id, out Player player);
        return player;
    }

    //Static method to remove a player by ID.
    public static bool RemovePlayer(ushort id)
    {
        if(s_Players.TryGetValue(id, out Player player))
        {
            s_Players.Remove(id);
            return true;
        }
        return false;
    }

    private static ushort s_localId = ushort.MaxValue; //ID of the local player.

    //Awake method.
    private void Awake()
    {
        s_PlayerPrefab = m_PlayerPrefab; //Assign the player prefab.
        Subscribe(); //Subscribe to network events
    }

    //OnDestroy method.
    private void OnDestroy()
    {
        Unsubscribe(); //Unsubscribe from network events.
    }

    //Subscribe to network events.
    private void Subscribe()
    {
        NetworkEvents.ConnectSuccess += SpawnInitalPlayer;
    }

    //Unsubscribe from network events.
    private void Unsubscribe()
    {
        NetworkEvents.ConnectSuccess -= SpawnInitalPlayer;
    }

    //Spawn the initial player when connection is successful.
    public void SpawnInitalPlayer(ushort id, string username)
    {
        //Instantiate player prefab.
        Player player = Instantiate(s_PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();

        //Set player name and initialize.
        player.name = $"{username} -- LOCAL PLAYER (WAITING FOR SERVER)";
        player.Init(id, username, true);
        s_localId = id; //Set local player ID.
        s_Players.Add(id, player); //Add player to dictionary.
        player.RequestInit(); //Request initialization from server.
    }

    //Initialize local player.
    private static void InitializeLocalPlayer()
    {
        Player local = s_Players[s_localId];
        local.name = $"{local.Username} -- {local.Id} -- LOCAL";
    }

    #region Messages

    /* ========== MESSAGE RECEIVING ==========*/
    [MessageHandler((ushort)ServerToClientMsg.ApproveLogin)]
    private static void ReceiveApproveLogin(Message msg)
    {
        bool approve = msg.GetBool();
        if(approve)
        {
            InitializeLocalPlayer(); //Initialize local player upon approval.
        }
    }

    #endregion
}