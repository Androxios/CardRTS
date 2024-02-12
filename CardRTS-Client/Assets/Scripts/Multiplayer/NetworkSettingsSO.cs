using UnityEngine;

//This scriptable object holds network settings such as port and IP address.
//It allows easy configuration of network settings for the client.
[CreateAssetMenu(menuName = "Multiplayer/Network/Settings")]
public class NetworkSettingsSO : ScriptableObject
{
    //Serialized fields for port and IP address.
    [SerializeField] private ushort m_Port = 7777;
    [SerializeField] private string m_ip = "127.0.0.1";

    //These fields are hidden in the inspector and are used internally to store local username and ID.
    [HideInInspector] public string LocalUsername;
    [HideInInspector] public ushort LocalId;

    //Properties to access port and IP address from other scripts.
    public ushort Port => m_Port;
    public string Ip => m_ip;
}