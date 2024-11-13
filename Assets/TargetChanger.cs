using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetChanger : MonoBehaviour
{
    RaycastHit hit;
    public GameObject player;
    public Canvas m_canvas;
    float plane_w;
    public GameObject cubePrefab;
    public Camera otherCamera;
    public GameObject real_player_ar;
    public NavMeshAgent agent;

    public float[] des_y;

    public GameObject arrowPrefab; // 화살표 프리팹 변수 추가

    private void Start()
    {
        des_y = new float[] { 0.3f, 6.48f, 12.66f, 18.84f, 25.02f, 31.2f };
        float plane_w = DatabaseSearch.plane_w;
        otherCamera = GameObject.Find("ScaleCamera").GetComponent<Camera>();
        if (otherCamera == null)
        {
            Debug.LogError("Other camera not found!");
        }
    }

    public void destination_pos(int st, float x, float y, int f)
    {
        float des_f_y = des_y[f - 1];

        Vector3 offMeshLinkDestination = new Vector3(real_player_ar.transform.position.x, real_player_ar.transform.position.y-0.55f, real_player_ar.transform.position.z);
        agent.Warp(offMeshLinkDestination);
        float clone_x = x;
        float clone_y = y;

        // 화살표 프리팹을 생성하여 사용
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.transform.LookAt(new Vector3(clone_x, des_f_y, clone_y));

        // 목적지 위치 설정
        Vector3 targetPosition = new Vector3(clone_x, des_f_y, clone_y);
        Debug.Log("real targetPosition >> " + targetPosition);
        this.transform.position = targetPosition;
        player.GetComponent<PathFinder>().makePath();
    }
}