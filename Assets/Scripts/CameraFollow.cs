using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform focus;
    public float smoothSpeed = 0.125f;
    public float xOffset, yOffset;

    void LateUpdate()
    {
        Vector3 currentPosition = new Vector3(focus.position.x + xOffset, focus.position.y + yOffset, transform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, currentPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}