using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneVision : MonoBehaviour
{
    public Camera droneCamera; // Attach your drone's camera here
    public Projector groundProjector; // Attach your projector here
    public float maxAltitude = 100.0f; // Set your maximum altitude
    public float newAngleOfView = 60.0f; // Set your new angle of view

    private float areaCoverage;
    private float consoleTimer = 0f;
    private float consoleInterval = 3f; // Console message interval in seconds

    private void FixedUpdate()
    {
        droneCamera.fieldOfView = newAngleOfView; // Adjusting the angle of view
        groundProjector.fieldOfView = newAngleOfView; // Also adjust the projector's field of view

        float altitude = transform.position.y;
        altitude = Mathf.Clamp(altitude, 0, maxAltitude);
        float FOV = droneCamera.fieldOfView;
        float areaRadius = 2.0f * altitude * Mathf.Tan(FOV * 0.5f * Mathf.Deg2Rad);
        areaCoverage = Mathf.PI * areaRadius * areaRadius;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            groundProjector.farClipPlane = hit.distance; // Set the projector's range to the distance to the ground

            if (Time.time - consoleTimer >= consoleInterval)
            {
                Debug.Log("Distance to ground: " + hit.distance);
                Debug.Log("Area Coverage: " + areaCoverage);
                consoleTimer = Time.time;
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingObject") // Assume you've tagged your moving objects as "MovingObject"
        {
            Debug.Log("Object entered area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingObject")
        {
            Debug.Log("Object exited area");
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingObject")
        {
            // Calculate direction to moving object and check if it's within FOV
            Vector3 dirToMovingObject = (other.transform.position - droneCamera.transform.position).normalized;
            float angle = Vector3.Angle(droneCamera.transform.forward, dirToMovingObject);

            if (angle <= droneCamera.fieldOfView / 2) // If within FOV
            {
                Debug.Log("Moving object entered the area");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MovingObject")
        {
            Debug.Log("Moving object exited the area");
        }
    }

}
