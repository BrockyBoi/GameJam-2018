using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSpin : MonoBehaviour
{
    [SerializeField]
    [Range(20, 100)]
    float rotateSpeed = 50;
    public Transform rotatePoint;

    void Update()
    {
        if (rotatePoint)
            transform.RotateAround(rotatePoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
        else transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }
}
