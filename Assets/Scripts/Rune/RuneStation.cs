using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStation : MonoBehaviour {

	static int numStations = 0;
	int stationIndex;
	[SerializeField]
	Material runeMat;
	public enum Colors{Red, Blue, NeonGreen, Purple}

	Colors correctColor;

	void Awake() {
		stationIndex = numStations;
		numStations ++;
	}

	void Start()
	{
		correctColor = RuneManager.runeKey[stationIndex];
		// assign correct material by asking RuneManager which rune to use
		runeMat = Resources.Load ("Materials/Runes/Rune"+stationIndex) as Material;
		Debug.Log(runeMat);
		transform.Find("Rune").GetComponent<MeshRenderer>().material = runeMat;
	}

	public void ButtonPressed(Colors colorPressed, DelegateManager.EmptyVoid successCallback, DelegateManager.EmptyVoid failCallback)
	{
		if(correctColor == colorPressed)
		{
			successCallback();
		}
		else
		{
			failCallback();
		}
	}

}
