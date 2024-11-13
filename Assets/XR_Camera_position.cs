using UnityEngine;
using UnityEngine.XR;

public class ElevatorController : MonoBehaviour
{
    public void MoveCameraToPosition(Vector3 newPosition)
    {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale); // ���÷� RoomScale ����
        Camera.main.transform.position = newPosition;
        Camera.main.transform.eulerAngles = Vector3.zero; // ȸ���� �ʱ�ȭ�ϰų� �ʿ信 ���� ����
    }
}
