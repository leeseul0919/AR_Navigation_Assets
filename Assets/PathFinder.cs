using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class PathFinder : MonoBehaviour
{
public GameObject target;
public NavMeshAgent agent;
//LineRenderer lr;
public GameObject arrowPrefab; // Prefab for the arrow object
public float arrowSpacing = 1f; // Spacing between each arrow
public float arrowScale = 0.4f; // Scale of the arrow object
public float arrowVisibleDistance = 9f; // 화살표가 보이는 거리를 조정할 값

private List<GameObject> arrows; // List to store the arrow objects

void Start()
{
    agent = this.GetComponent<NavMeshAgent>();
    //lr = this.GetComponent<LineRenderer>();
    //lr.startWidth = lr.endWidth = 0.1f;
    //lr.material.color = Color.blue;
    //lr.enabled = false;
    arrows = new List<GameObject>();
}

public void makePath()
{
    //lr.enabled = true;
    StartCoroutine(makePathCoroutine());
}

IEnumerator makePathCoroutine()
{
    agent.SetDestination(target.transform.position);
    Debug.Log("in PathFinder target positioin >> " + target.transform.position);
    //lr.SetPosition(0, this.transform.position);

    yield return new WaitForSeconds(2f);

    drawPath();
}

void Update()
{
    UpdateArrowVisibility();
}

void drawPath()
{
    //int length = agent.path.corners.Length;

    //lr.positionCount = length;
    //for (int i = 1; i < length; i++)
    //    lr.SetPosition(i, agent.path.corners[i]);
        
        // Clear previous arrows
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();

        // Calculate the total distance of the path
        float totalDistance = 0f;
        Vector3[] pathCorners = agent.path.corners;
        for (int i = 1; i < pathCorners.Length; i++)
        {
            totalDistance += Vector3.Distance(pathCorners[i - 1], pathCorners[i]);
        }

        // Calculate the number of arrows needed
        int numArrows = Mathf.CeilToInt(totalDistance / arrowSpacing);

        // Create arrows along the path
        for (int i = 1; i < pathCorners.Length; i++)
        {
            Vector3 direction = pathCorners[i] - pathCorners[i - 1];
            Quaternion rotation = Quaternion.LookRotation(direction);
            rotation *= Quaternion.Euler(0f, -180f, 0f); // y축으로 90도 회전
            float segmentDistance = Vector3.Distance(pathCorners[i - 1], pathCorners[i]);
            int numSegments = Mathf.CeilToInt(segmentDistance / arrowSpacing);

            for (int j = 0; j < numSegments; j++)
            {
                float t = (float)j / numSegments;
                Vector3 position = Vector3.Lerp(pathCorners[i - 1], pathCorners[i], t);
                position.y += 0.5f; // y축으로 만큼 높이 올리기
                GameObject arrow = Instantiate(arrowPrefab, position, rotation);
                arrow.transform.localScale = new Vector3(arrowScale, arrowScale, arrowScale);

                arrows.Add(arrow);
            }
        }
    }
    void UpdateArrowVisibility()
    {
        foreach (GameObject arrow in arrows)
        {
            float distance = Vector3.Distance(Camera.main.transform.position, arrow.transform.position);

            if (distance > arrowVisibleDistance)
            {
                arrow.SetActive(false);
            }
            else
            {
                arrow.SetActive(true);
            }
        }
    }
}