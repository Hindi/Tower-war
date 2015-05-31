using UnityEngine;
using UnityEditor;
using System.Collections;

public class MapBorder : MonoBehaviour
{
    [SerializeField]
    private CameraMovement camera;

    void OnEnable()
    {

    } 

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(-camera.Width / 2, camera.Height / 2), new Vector3(camera.Width / 2, camera.Height / 2));
        Gizmos.DrawLine(new Vector3(-camera.Width / 2, -camera.Height / 2), new Vector3(camera.Width / 2, -camera.Height / 2));
        Gizmos.DrawLine(new Vector3(camera.Width / 2, -camera.Height / 2), new Vector3(camera.Width / 2, camera.Height / 2));
        Gizmos.DrawLine(new Vector3(-camera.Width / 2, -camera.Height / 2), new Vector3(-camera.Width / 2, -camera.Height / 2));
    }
}
