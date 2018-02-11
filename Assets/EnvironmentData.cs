using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentData : MonoBehaviour {

	public Transform mainEnvironment, duplicateEnvironment;
	public static Vector3 MAP_OFFSET{get; private set;}

	void Awake()
	{
		MAP_OFFSET = duplicateEnvironment.position - mainEnvironment.position;
	}
	

}
