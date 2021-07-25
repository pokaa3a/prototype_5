using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    // float velocity = 0f;
    Vector3 velocity = Vector2.zero;
    float cameraHeight = 10f;

    [SerializeField]
    float smoothTime = 0.3f;
    [SerializeField]
    float cameraAbove = 2f;

    //----------------------------------------------------------------------------------------------
    void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.TransformPoint(new Vector3(0, -cameraAbove, -cameraHeight));
        targetPosition.x = transform.position.x;
        transform.position = Vector3.SmoothDamp(
            transform.position, targetPosition, ref velocity, smoothTime);
    }    
}
