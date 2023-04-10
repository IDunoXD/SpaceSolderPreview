using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float cameraSpeed;
    [SerializeField] Vector3 Offset;

    private Vector3 _followPosition;

    void LateUpdate()
    {
        _followPosition = target.position + Vector3.Scale(transform.forward, Offset);
        transform.position = Vector3.Lerp(transform.position, _followPosition, Time.deltaTime * cameraSpeed);
    }
}
