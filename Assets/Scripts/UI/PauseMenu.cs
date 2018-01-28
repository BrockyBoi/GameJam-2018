using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	static PauseMenu Instance{get;set;}
    public GameObject menu;
    bool paused;
    public static bool Paused
    {
        get { return Instance.paused; }
        private set
        {
            Instance.paused = value;
            Cursor.visible = value;

            if (value)
            {
                Instance.menu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Instance.menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

	void Awake()
	{
		Instance = this;
        Cursor.visible = false;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }
    }

    public void PressResume()
    {
        Paused = false;
    }

    public void PressExit()
    {
        Application.Quit();
    }
}
