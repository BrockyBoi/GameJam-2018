using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUI : MonoBehaviour {

	[SerializeField]
	int index;
	// Use this for initialization
	void Start () {
		GetComponent<Image>().material = Resources.Load("Materials/Colors/"+RuneManager.RuneKey[index].ToString()) as Material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
