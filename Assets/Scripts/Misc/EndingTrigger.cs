using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour {

	public GameObject endingCanvas;

	void OnTriggerEnter(Collider other)
	{
		RuneManager.ClearDict();
		endingCanvas.SetActive(true);
	}
}
