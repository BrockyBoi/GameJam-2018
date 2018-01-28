using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OriginalObject<T> : MonoBehaviour {

	Queue<T> objectData = new Queue<T>();

	protected virtual void Update () 
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
