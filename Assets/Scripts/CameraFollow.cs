using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.5f;
    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - target.transform.position;
    }

    void Update ()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);

        //transform.LookAt(target);
    }
}
