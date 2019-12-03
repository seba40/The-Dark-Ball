using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform focus;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

        void LateUpdate()
    {
        Vector3 currentPosition = focus.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, currentPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
