using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAlien : OffsetObject<Alien, LocationData>
{

    protected override void Start()
    {
        base.Start();
        original = Alien.Instance;
    }
    protected override void SetData(LocationData data)
    {
        Vector3 newPos = data.loc + EnvironmentData.MAP_OFFSET;
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        transform.rotation = data.quat;
    }
}
