using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffsetObject<T, R> : MonoBehaviour where T : OriginalObject<R>
{
	#region Variables
	public static readonly Vector3 MAP_OFFSET = new Vector3(0, 0, 50f);
    [SerializeField]
    [Range(1, 10)]
    int secondsOff = 5;

    [SerializeField]
    T original;

    bool isReady = false;
	#endregion

    protected void Start()
    {
        Invoke("SetIsReady", secondsOff);
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
