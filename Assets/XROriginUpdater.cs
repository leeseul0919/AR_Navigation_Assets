using UnityEngine;

public class XROriginUpdater : MonoBehaviour
{
    public Transform mainCamera; // Main Camera 객체
    public Transform xrOrigin;   // XR Origin 객체

    void Update()
    {
        if (mainCamera != null && xrOrigin != null)
        {
            // Main Camera의 위치와 회전을 가져옴
            Vector3 cameraPosition = mainCamera.position;
            Quaternion cameraRotation = mainCamera.rotation;

            // XR Origin의 위치와 회전을 Main Camera의 위치와 회전으로 업데이트
            xrOrigin.position = cameraPosition;
            xrOrigin.rotation = cameraRotation;
        }
    }
}
