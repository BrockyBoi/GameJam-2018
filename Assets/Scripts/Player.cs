﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : OriginalObject<LocationData>
{
	public static Player Instance{get; private set;}
	public static event DelegateManager.EmptyVoid EOnPlayerDeath;
	void Awake()
	{
		Instance = this;
	}
    protected override void AddData()
    {
        EnqueueData(new LocationData(transform.position, transform.rotation));
    }

	public void KillPlayer()
	{
		if(EOnPlayerDeath != null)
		{
			EOnPlayerDeath();
		}
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

