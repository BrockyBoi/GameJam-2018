using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : OriginalObject<LightAndLoc>
{
    public static Player Instance { get; private set; }
    public static bool PlayerSpawned { get; private set; }
    public static event DelegateManager.EmptyVoid EOnPlayerDeath;

    [SerializeField]
    GameObject otherPlayerPrefab;

    Light light;
    void Awake()
    {
        Instance = this;
        light = GetComponentInChildren<Light>();

        Instantiate(otherPlayerPrefab);
        GameManager.SpawnAlien();

        PlayerSpawned = true;
        Camera.main.enabled = false;
    }
    protected override void AddData()
    {
        EnqueueData(new LightAndLoc(new LocationData(transform), new LightFlickerData(light.enabled)));
    }

    public void KillPlayer()
    {
        if (EOnPlayerDeath != null)
        {
            EOnPlayerDeath();
        }
    }
}

public struct LocationData
{
    public Vector3 loc { get; private set; }
    public Quaternion quat { get; private set; }

    public LocationData(Transform trans)
    {
        loc = trans.position;
        quat = trans.rotation;
    }
}

public struct LightFlickerData
{
    public bool lightOn { get; private set; }
    public LightFlickerData(bool on)
    {
        lightOn = on;
    }
}

public struct LightAndLoc
{
    public LocationData locData { get; private set; }
    public LightFlickerData flickData { get; private set; }

    public LightAndLoc(LocationData lD, LightFlickerData lFD)
    {
        locData = lD;
        flickData = lFD;
    }
}

