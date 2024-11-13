using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCamera : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        QualitySettings.shadowDistance = 0f;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 3.5f, Player.transform.position.z);
    }
}
