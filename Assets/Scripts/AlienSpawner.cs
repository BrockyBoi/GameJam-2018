using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour {
	static AlienSpawner instance;
	static AlienSpawner Instance 
	{
		get
		{
			if(instance == null)
			{
				instance = new GameObject("AlienSpawner", typeof(AlienSpawner)).GetComponent<AlienSpawner>();
			}

			return instance;
		}
	}

	public static void SpawnAlien()
	{
		
	}
}
