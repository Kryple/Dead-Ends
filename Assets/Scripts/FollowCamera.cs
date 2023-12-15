using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Public variables
    public Transform target;
    private float smoothTime = 0.7f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    // Update is called once per frame

    private void Start()
    {
        
    }

    void Update()
    {
        
        
        // Calculate the desired position
        Vector3 desiredPosition = target.position + offset;
        
        

        // Smoothly move the camera towards the desired position
        //transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        // Look at the target
        //transform.LookAt(target);
    }
}