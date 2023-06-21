using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraGizmoDrawer : MonoBehaviour
{
    private Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void OnDrawGizmos()
    {
        if (camera != null)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);
        }
    }
}
