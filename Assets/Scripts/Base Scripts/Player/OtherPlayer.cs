using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : OffsetObject<Player, LocationData>
{
    protected override void SetData(LocationData data)
    {
        transform.position = data.loc + MAP_OFFSET;
        transform.rotation = data.quat;
    }

}
