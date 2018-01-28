using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAlien : OffsetObject<Alien, LocationData> {
    protected override void SetData(LocationData data)
    {
        transform.position = data.loc + MAP_OFFSET;
        transform.rotation = data.quat;
    }
}
