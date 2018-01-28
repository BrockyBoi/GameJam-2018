using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    CanvasGroup group;
    public List<Button> menuButtons;
    void OnEnable()
    {
        Player.EOnPlayerDeath += ShowDeathScreen;
    }

    void Awake()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;

        foreach (var b in menuButtons)
            b.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Player.EOnPlayerDeath -= ShowDeathScreen;
    }

    void ShowDeathScreen()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        foreach (var b in menuButtons)
            b.gameObject.SetActive(true);
			
        while (group.alpha < 1.0f)
        {
            group.alpha += 0.01f;
            yield return null;
        }
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
