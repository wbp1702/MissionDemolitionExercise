using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static GameObject PointOfIntrest;

    [Header("Inscribed")]
    public float easing = 0.05f;

    [Header("Dynamic")]
    public float cameraZPosition;
    
    void Awake()
    {
        cameraZPosition = transform.position.z;
    }

    void FixedUpdate()
    {
        if (PointOfIntrest == null) return;

        Vector3 destination = PointOfIntrest.transform.position;
        destination.z = cameraZPosition;

        transform.position = Vector3.Lerp(transform.position, destination, easing);
    }
}
