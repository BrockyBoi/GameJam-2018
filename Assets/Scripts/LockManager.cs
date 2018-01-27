using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    static LockManager instance;
    static LockManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Lock Manager", typeof(LockManager)).GetComponent<LockManager>();
            }
            return instance;
        }
    }
    uint totalLocks = 0;
    uint correctLocksPressed = 0;
    public bool PressedAllButtons { get { return correctLocksPressed == totalLocks; } }
    // Use this for initialization
    void Start()
    {
        totalLocks = LockButton.lockNum;
    }

    public static void CorrectButtonPressed()
    {
        Instance.correctLocksPressed++;
    }

    public static void WrongButtonPressed()
    {

    }
}
