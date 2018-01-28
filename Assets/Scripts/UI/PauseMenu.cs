using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static PauseMenu Instance { get; set; }
    public GameObject menu;
    bool paused = false;
    bool playerDead = false;
    public static event DelegateManager.EmptyVoid EOnNoLongerPaused, EOnPause;

    public static bool Paused
    {
        get { return Instance.paused; }
        private set
        {
            if (Instance.playerDead)
                return;

            Instance.paused = value;
            Cursor.visible = value;

            if (value)
            {
                Instance.menu.SetActive(true);
                Time.timeScale = 0;
                if(EOnPause != null)
                    EOnPause();
            }
            else
            {
                Instance.menu.SetActive(false);
                Time.timeScale = 1;
                if(EOnNoLongerPaused != null)
                    EOnNoLongerPaused();
            }
        }
    }

    void OnEnable()
    {
        Player.EOnPlayerDeath += PlayerIsDead;
    }

    void OnDisable()
    {
        Player.EOnPlayerDeath -= PlayerIsDead;
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

    void PlayerIsDead()
    {
        playerDead = true;
        Cursor.visible = true;
    }
}
