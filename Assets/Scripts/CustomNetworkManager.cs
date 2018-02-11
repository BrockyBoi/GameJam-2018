using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public static CustomNetworkManager Instance { get; private set; }
    public static NetworkDataStruct hostPlayer, clientPlayer;
    short playerCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
			NetworkManager.singleton = this;
        }


    }

    public override void OnClientConnect(NetworkConnection conn)
    {
		base.OnClientConnect(conn);
        Debug.Log("Player joined");
        //OnServerAddPlayer(conn, playerCount++);
        if (GameManager.IsServer)
        {
            CreateStandardPlayer();
        }
        else
        {
            CreateOverheadPlayer();
        }
        Camera.main.enabled = false;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        if (GameManager.IsServer)
        {
            player = CreateStandardPlayer();
            hostPlayer = new NetworkDataStruct(conn, playerControllerId);
        }
        else
        {
            player = CreateOverheadPlayer();
            clientPlayer = new NetworkDataStruct(conn, playerControllerId);
        }


        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public GameObject CreateStandardPlayer()
    {
        return Instantiate(playerPrefab, NetworkManager.singleton.spawnPrefabs[0].transform.position, Quaternion.identity);
    }

    public GameObject CreateOverheadPlayer()
    {
        GameManager.EnableOverheadCamera();
        return new GameObject("Player");
    }
}

public struct NetworkDataStruct
{
    public NetworkConnection connection;
    public short playerID;

    public NetworkDataStruct(NetworkConnection conn, short id)
    {
        connection = conn;
        playerID = id;
    }
}
