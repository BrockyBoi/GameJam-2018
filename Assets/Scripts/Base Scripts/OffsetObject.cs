using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffsetObject<T, R> : MonoBehaviour where T : OriginalObject<R>
{
	#region Variables
	protected Vector3 MAP_OFFSET;
    [SerializeField]
    [Range(1, 10)]
    int secondsOff = 5;

    [SerializeField]
    T original;

    bool isReady = false;
	#endregion

    protected void Start()
    {
        MAP_OFFSET = transform.position - original.transform.position;
        Invoke("SetIsReady", secondsOff);
    }

    protected void Update()
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
