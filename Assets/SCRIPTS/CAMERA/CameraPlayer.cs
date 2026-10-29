using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform point;

    public Vector3 offset;
    public float smoothSpeed;

    [Header("Detectors")]
    public LayerMask wallLayer;
    public Vector4 distance;

    private Vector3 velocity;


    public void Start()
    {
        
    }

    // void LateUpdate()
    // {
    //     transform.position = point.position + offset;
    // }

    bool ForwardDetector() => Physics.Raycast(point.position, Vector3.forward, distance.x, wallLayer);
    bool BackDetector() => Physics.Raycast(point.position, -Vector3.forward, distance.y, wallLayer);
    bool RightDetector() => Physics.Raycast(point.position, Vector3.right, distance.z, wallLayer);
    bool LeftDetector() => Physics.Raycast(point.position, -Vector3.right, distance.w, wallLayer);

    public void FixedUpdate()
    {
        Vector3 desired = new Vector3(
            point.position.x + offset.x,
            transform.position.y + offset.y,
            point.position.z + offset.z
        );
        
        Vector3 finalPos = transform.position;

        Debug.DrawRay(point.position, Vector3.forward * distance.x);
        Debug.DrawRay(point.position, -Vector3.forward * distance.y);
        Debug.DrawRay(point.position, Vector3.right * distance.z);
        Debug.DrawRay(point.position, -Vector3.right * distance.w);

        if(!ForwardDetector() && !BackDetector())
        {
            finalPos.z = desired.z;
        }
        
        if(!RightDetector() && !LeftDetector())
        {
            finalPos.x = desired.x;
        }
        
        transform.position = Vector3.Lerp(transform.position, finalPos, smoothSpeed * Time.deltaTime);  
    }
}
