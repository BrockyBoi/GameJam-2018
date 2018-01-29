using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public GameObject mainMenuParent,creditsParent;

	public void PressPlay()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}

	public void PressCredits()
	{
		creditsParent.SetActive(true);
		mainMenuParent.SetActive(false);
	}

	public void PressBackCredits()
	{
		creditsParent.SetActive(false);
		mainMenuParent.SetActive(true);
	}

	public void PressQuit()
	{
		Application.Quit();
	}
}
