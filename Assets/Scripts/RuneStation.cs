using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStation : MonoBehaviour {

	[SerializeField]
	Material runeMat;
	public enum Colors{Red, Blue, NeonGreen, Purple}

	[SerializeField]
	Colors correctColor;

	void Start()
	{
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
