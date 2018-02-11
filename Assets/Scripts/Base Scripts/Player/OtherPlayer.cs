using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class OtherPlayer : OffsetObject<Player, LightAndLoc>
{
    Light light;
    void Awake()
    {
        light = GetComponentInChildren<Light>();
    }

    protected override void Start()
    {
        original = Player.Instance;
        transform.position = original.transform.position + EnvironmentData.MAP_OFFSET;

        base.Start();
    }
    protected override void SetData(LightAndLoc data)
    {
        transform.position = data.locData.loc + EnvironmentData.MAP_OFFSET;
        transform.rotation = data.locData.quat;

        light.enabled = data.flickData.lightOn;
    }

}
