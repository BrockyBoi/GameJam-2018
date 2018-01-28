using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

	CanvasGroup group;
	void OnEnable()
	{
		Player.EOnPlayerDeath += ShowDeathScreen;
	}

	void Awake() {
		group = GetComponent<CanvasGroup>();
		group.alpha = 0;
	}

	void OnDisable()
	{
		Player.EOnPlayerDeath -= ShowDeathScreen;
	}

	void ShowDeathScreen()
	{
		StartCoroutine(FadeToBlack());
	}

	IEnumerator FadeToBlack() {
		while(group.alpha < 1.0f) {
			group.alpha += 0.01f;
			yield return null;
		}
	}

	public void Reset() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Exit() {
		Application.Quit();
	}
}
