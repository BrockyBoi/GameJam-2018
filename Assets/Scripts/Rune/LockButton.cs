using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : MonoBehaviour
{
    RuneStation station;
    public static uint lockNum { get; private set; }
    [SerializeField]
    RuneStation.Colors buttonColor;

    DelegateManager.EmptyVoid success, failure;
    bool animating;
    void Awake()
    {
        lockNum++;
        station = GetComponentInParent<RuneStation>();

        success += LockManager.CorrectButtonPressed;
        success += delegate { if (!station.correctButtonPressed) { RuneManager.CorrectbuttonPressed();} };
        success += () => station.correctButtonPressed = true;
        failure += AlienSpawner.SpawnAlien;
    }

    public void PressButton()
    {
        station.ButtonPressed(buttonColor, success, failure);
        if (!animating)
            StartCoroutine(ButtonPressAnim());
    }

    IEnumerator ButtonPressAnim()
    {
        animating = true;
        float animTime = 1, currTime = 0;
        Vector3 startPos = transform.localPosition, endPos = startPos - new Vector3(.2f, 0, 0);
        while (currTime < animTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, currTime / animTime);
            currTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;

        currTime = 0;

        while (currTime < animTime)
        {
            transform.localPosition = Vector3.Lerp(endPos, startPos, currTime / animTime);
            currTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startPos;
        animating = false;
    }
}
