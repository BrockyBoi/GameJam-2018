using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class OffsetObject<T, R> : MonoBehaviour where T : OriginalObject<R>
{
    #region Variables

    public static int SecondsOff = 3;

    protected T original;

    bool isReady = false;
    #endregion

    protected void OnEnable()
    {
        PauseMenu.EOnNoLongerPaused += WaitForData;
        PauseMenu.EOnPause += StopWait;
    }

    protected void OnDisable()
    {
        PauseMenu.EOnNoLongerPaused -= WaitForData;
        PauseMenu.EOnPause -= StopWait;
    }

    protected virtual void Start()
    {
        WaitForData();
    }

    protected void WaitForData()
    {
        Invoke("SetIsReady", SecondsOff);
    }

    protected void StopWait()
    {
        CancelInvoke("SetIsReady");
    }

    protected void FixedUpdate()
    {
        if (!isReady)
            return;

        SetData(original.GetData());
    }

    protected void SetIsReady()
    {
        isReady = true;
    }

    protected abstract void SetData(R data);
}
