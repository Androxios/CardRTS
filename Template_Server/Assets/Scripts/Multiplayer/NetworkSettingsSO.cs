using UnityEngine;

//ScriptableObject class for storing network settings.
//This class allows easy configuration of server settings.
[CreateAssetMenu(menuName = "Multiplayer/Network/Settings")]
public class NetworkSettingsSO : ScriptableObject
{
    //Port number for the server.
    public ushort Port = 7777;

    //Maximum number of players allowed on the server.
    public ushort MaxPlayers = 10;
}