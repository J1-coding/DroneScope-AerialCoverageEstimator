using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVCamera : MonoBehaviour
{
    public float newAngleOfView = 60.0f; // Set your new angle of view
    public Camera droneCamera;

    private void FixedUpdate()
    {
        droneCamera.fieldOfView = newAngleOfView;
    }

}
