using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : OriginalObject<LocationData>
{
    protected override void AddData()
    {
        EnqueueData(new LocationData(transform.position, transform.rotation));
    }
}

public struct LocationData
{
	public Vector3 loc;
	public Quaternion quat;

	public LocationData(Vector3 vector, Quaternion quaternion)
	{
		loc = vector;
		quat = quaternion;
	}
}

