using UnityEngine;
using UnityEngine.XR;

public class ElevatorController : MonoBehaviour
{
    public void MoveCameraToPosition(Vector3 newPosition)
    {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale); // 예시로 RoomScale 설정
        Camera.main.transform.position = newPosition;
        Camera.main.transform.eulerAngles = Vector3.zero; // 회전을 초기화하거나 필요에 따라 설정
    }
}
