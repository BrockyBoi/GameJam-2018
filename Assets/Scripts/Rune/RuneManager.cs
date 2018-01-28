using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour {
	public static Dictionary<int, RuneStation.Colors> runeKey;
	// Use this for initialization
	
	void Awake () {
		runeKey = new Dictionary<int, RuneStation.Colors>();
		// initialize runekey randomly
		RuneStation.Colors[] c = {RuneStation.Colors.Blue, RuneStation.Colors.Red, RuneStation.Colors.NeonGreen, RuneStation.Colors.Purple};
		List<RuneStation.Colors> colors = new List<RuneStation.Colors>(c);
		int i = 0;
		while(i < 4) {
			int k = Random.Range(0,colors.Count);
			runeKey[i] = colors[k];
			colors.RemoveAt(k);
			i++;
		} 

	}
	
}
