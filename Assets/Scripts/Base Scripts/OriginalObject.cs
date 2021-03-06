﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class OriginalObject<T> : MonoBehaviour {

	Queue<T> objectData = new Queue<T>();

	protected virtual void FixedUpdate () 
	{
		AddData();
	}

	protected abstract void AddData();

	protected void EnqueueData(T data)
	{
		objectData.Enqueue(data);
	}
	public T GetData()
	{
		return objectData.Dequeue();
	}
}


