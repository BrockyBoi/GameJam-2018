﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStation : MonoBehaviour {

	public bool correctButtonPressed = false;
	static int numStations = 0;
	int stationIndex;

	public enum Colors{Red, Blue, NeonGreen, Purple}

	Colors correctColor;

	public Transform runeSpot;

	void Awake() {
		stationIndex = numStations;
		numStations ++;
	}

	void Start()
	{
		correctColor = RuneManager.RuneKey[stationIndex];
		GameObject rune = Instantiate(Resources.Load("Runes/Rune " + (stationIndex + 1)) as GameObject, Vector3.zero, Quaternion.identity);
		Vector3 scale = rune.transform.localScale;
		rune.transform.SetParent(transform);
		rune.transform.localScale = scale;
		rune.transform.localPosition = new Vector3(4.47f, -.611f, .204f);
		rune.transform.parent = null;
		rune.transform.localScale = scale;
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
