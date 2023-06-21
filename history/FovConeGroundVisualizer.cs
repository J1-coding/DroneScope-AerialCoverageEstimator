using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FovConeGroundVisualizer : MonoBehaviour
{
    public float maxDistance = 100f;
    public float fieldOfView = 60f;
    public LayerMask groundMask; // Set this to the layer your ground is on in the Unity Editor.

    private Camera _camera;

    private void OnValidate()
    {
        _camera = GetComponent<Camera>();
        if (_camera != null)
        {
            _camera.fieldOfView = fieldOfView;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawFovCone();
    }

    private void DrawFovCone()
    {
        float halfFov = fieldOfView * 0.5f;
        Vector3 to = transform.forward * maxDistance;

        Quaternion rightRotation = Quaternion.AngleAxis(halfFov, transform.up);
        Quaternion leftRotation = Quaternion.AngleAxis(-halfFov, transform.up);

        Vector3 rightRay = rightRotation * to;
        Vector3 leftRay = leftRotation * to;

        Gizmos.DrawRay(transform.position, to);
        DrawRayToGround(rightRay);
        DrawRayToGround(leftRay);

        Vector3 prevPoint = GetGroundIntersectionPoint(rightRay);
        for (int i = 1; i <= 30; i++)
        {
            float t = (float)i / 30f;
            Vector3 p = Vector3.Slerp(rightRay, leftRay, t);
            Vector3 nextPoint = GetGroundIntersectionPoint(p);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }

    private void DrawRayToGround(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundMask))
        {
            Gizmos.DrawRay(ray.origin, hit.point - ray.origin);
        }
    }

    private Vector3 GetGroundIntersectionPoint(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
