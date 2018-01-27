using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : OriginalObject<LocationData>
{
    protected override void AddData()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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

