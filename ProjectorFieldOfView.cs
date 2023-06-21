using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ProjectorFieldOfView : MonoBehaviour
{
    public Projector projector;

    void Update()
    {
        if (projector != null)
        {
            projector.fieldOfView = GetComponent<Camera>().fieldOfView;
            projector.aspectRatio = GetComponent<Camera>().aspect;
            projector.transform.position = transform.position;
            projector.transform.rotation = transform.rotation;
        }
    }
}
