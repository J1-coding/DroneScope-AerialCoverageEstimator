using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFOVLines : MonoBehaviour
{
    public Transform ground;
    public LineRenderer lineRenderer;
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        DrawFOVLines();
    }

    void DrawFOVLines()
    {
        float maxDistance = 1000; // Maximum distance at which the lines will be drawn
        Vector3[] corners = new Vector3[5];

        corners[0] = GetWorldPosition(new Vector3(0, 0, cam.farClipPlane), maxDistance);
        corners[1] = GetWorldPosition(new Vector3(0, 1, cam.farClipPlane), maxDistance);
        corners[2] = GetWorldPosition(new Vector3(1, 1, cam.farClipPlane), maxDistance);
        corners[3] = GetWorldPosition(new Vector3(1, 0, cam.farClipPlane), maxDistance);
        corners[4] = corners[0]; // Close the loop

        if (lineRenderer.positionCount != corners.Length)
            lineRenderer.positionCount = corners.Length;

        for (int i = 0; i < corners.Length; i++)
        {
            lineRenderer.SetPosition(i, corners[i]);
        }
    }

    Vector3 GetWorldPosition(Vector3 screenPosition, float maxDistance)
    {
        Ray ray = cam.ViewportPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            return hit.point;
        }
        else
        {
            return ray.GetPoint(maxDistance);
        }
    }
}
