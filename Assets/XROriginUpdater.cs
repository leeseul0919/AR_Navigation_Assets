using UnityEngine;

public class XROriginUpdater : MonoBehaviour
{
    public Transform mainCamera; // Main Camera ��ü
    public Transform xrOrigin;   // XR Origin ��ü

    void Update()
    {
        if (mainCamera != null && xrOrigin != null)
        {
            // Main Camera�� ��ġ�� ȸ���� ������
            Vector3 cameraPosition = mainCamera.position;
            Quaternion cameraRotation = mainCamera.rotation;

            // XR Origin�� ��ġ�� ȸ���� Main Camera�� ��ġ�� ȸ������ ������Ʈ
            xrOrigin.position = cameraPosition;
            xrOrigin.rotation = cameraRotation;
        }
    }
}
