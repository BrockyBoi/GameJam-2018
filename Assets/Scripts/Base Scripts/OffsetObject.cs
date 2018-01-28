using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffsetObject<T, R> : MonoBehaviour where T : OriginalObject<R>
{
    #region Variables
    protected Vector3 MAP_OFFSET;

    public static int SecondsOff = 3;

    [SerializeField]
    T original;

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

    protected void Start()
    {
        MAP_OFFSET = transform.position - original.transform.position;
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
