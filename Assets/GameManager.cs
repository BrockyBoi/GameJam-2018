using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool ServerIsPlayer { get; private set; }
    public static bool IsServer { get { return Instance.isServer; } }
    public Transform alienSpawn;
    public GameObject alienPrefab;
    Camera overHeadCamera;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        ServerIsPlayer = true;
        overHeadCamera = GameObject.Find("OverheadCamera").GetComponent<Camera>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewScene;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewScene;
    }

    public static void GameEnded()
    {
        ServerIsPlayer = !ServerIsPlayer;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public override void OnStartLocalPlayer()
	{
		Debug.Log("Player added");
	}

    void OnNewScene(Scene scene, LoadSceneMode mode)
    {
        var manager = CustomNetworkManager.Instance;
        if (!manager)
            manager = GameObject.Find("Network Manager").GetComponent<CustomNetworkManager>();


        GameObject player;
        if (isServer)
        {
            if (ServerIsPlayer)
            {
                player = manager.CreateStandardPlayer();
            }
            else
            {
                player = manager.CreateOverheadPlayer();
            }
            // var data = CustomNetworkManager.hostPlayer;
            // NetworkServer.ReplacePlayerForConnection(data.connection, player, data.playerID);
        }
        else
        {
            if (ServerIsPlayer)
            {
                player = manager.CreateOverheadPlayer();
            }
            else
            {
                player = manager.CreateStandardPlayer();
            }
            // var data = CustomNetworkManager.clientPlayer;
            // NetworkServer.ReplacePlayerForConnection(data.connection, player, data.playerID);
        }

    }

    public static void EnableOverheadCamera()
    {
        Instance.overHeadCamera.enabled = true;
    }

    public static void SpawnAlien()
    {
        Instantiate(Instance.alienPrefab, Instance.alienSpawn.position, Quaternion.identity);
    }
}
