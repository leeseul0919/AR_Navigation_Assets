using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using WebSocketSharp;
using TMPro;

public class obs_type
{
    public int obs_id;
    public int floor;
    public GameObject obs;
    public Vector3 start;
    public Vector3 end;
    public obs_type(int id, int create_floor, GameObject cb, Vector3 start_data, Vector3 end_data)
    {
        obs_id = id;
        floor = create_floor;
        obs = cb;
        start = start_data;
        end = end_data;
    }
}
public class CubeCreator : MonoBehaviour
{
    public Camera obstacleCamera;
    public GameObject cubePrefab;
    public Button createButton;

    private Vector3 startPos;
    private bool isStartSet = false;
    public bool iscreate = false;

    public WebSocket webSocket;

    public Transform obs_content;
    public GameObject obs_clone;

    public List<obs_type> Obstacles = new List<obs_type>();
    public int progress_st;
    public Select_obstaclefloor current_floor;
    public float[] des_y;
    public void Start()
    {
        des_y = new float[] { 0.3f, 6.48f, 12.66f, 18.84f, 25.02f, 31.2f };
        createButton.onClick.AddListener(OnCreateButton);
        webSocket = new WebSocket("wss://port-0-docker-node-be-1ru12mlvsjr55i.sel5.cloudtype.app/:3000");
        webSocket.OnClose += OnClose;
    }

    public void update_obs_list()
    {
        Debug.Log("update_obs_list");
        foreach (Transform child in obs_content)
        {
            Destroy(child.gameObject);
        }
        int i = 1;
        foreach (obs_type obstacle in Obstacles)
        {
            GameObject add_obs_clone = Instantiate(obs_clone, obs_content);
            TextMeshProUGUI obs_text = add_obs_clone.GetComponentInChildren<TextMeshProUGUI>();
            obs_text.text = $"({obstacle.obs_id}: {obstacle.floor}F), {obstacle.obs.transform.position.x}, {obstacle.obs.transform.position.y}, {obstacle.obs.transform.position.z}";
            Button obs_delete_button = add_obs_clone.GetComponentInChildren<Button>();
            obs_delete_button.onClick.AddListener(() => DeleteObstacle(obstacle, add_obs_clone));
            i++;
        }
    }
    public void UpdateObstacleVisibility(int selectedFloor)
    {
        foreach (obs_type obstacle in Obstacles)
        {
            obstacle.obs.GetComponent<MeshRenderer>().enabled = (obstacle.floor == selectedFloor);
        }
    }
    public void DeleteObstacle(obs_type obstacle, GameObject clone)
    {
        // ���� ������Ʈ ����Ʈ���� ����
        Obstacles.Remove(obstacle);
        Destroy(obstacle.obs);
        // UI���� �ش� �׸� ����
        Destroy(clone);
        webSocket.Connect();
        progress_st = 3;
        webSocket.Send($"{{\"progress_st\": \"{progress_st}\", \"obs_id\": \"{obstacle.obs_id}\",\"floor\": \"{obstacle.floor}\", \"start_x\": \"{obstacle.start.x}\", \"start_z\": \"{obstacle.start.z}\", \"end_x\": \"{obstacle.end.x}\", \"end_z\": \"{obstacle.end.z}\"}}");
        update_obs_list();
    }
    public void OnCreateButton()
    {
        iscreate = true;
    }
    void Update()
    {
        if (iscreate == true)
        {
            // ȭ�鿡 ��ġ �Է��� �ִ��� Ȯ��
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ������

                // ��ġ�� ���۵��� ��
                if (touch.phase == TouchPhase.Began)
                {
                    // �������� �������� �ʾҴٸ�, �������� ����
                    if (!isStartSet)
                    {
                        startPos = GetWorldPosition(touch.position);
                        Debug.Log("���� ��ǥ (��ũ��): " + touch.position + ", ���� ��ǥ (����): " + startPos);
                        isStartSet = true;
                    }
                    else // �������� �̹� ������ ���¶��, �������� �����ϰ� ť�긦 ����
                    {
                        Vector3 endPos = GetWorldPosition(touch.position);
                        Debug.Log("���� ��ǥ (��ũ��): " + touch.position + ", ���� ��ǥ (����): " + endPos);
                        CreateCube(startPos, endPos, Obstacles.Count + 1);

                        isStartSet = false; // ���� �Է��� ���� ������ ������ �ʱ�ȭ
                        iscreate = false;
                    }
                }
            }
        }
    }

    // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ�ϴ� �Լ�
    Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        Ray ray = obstacleCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        // ����� ���̾� ����ũ ����
        LayerMask layerMask = LayerMask.GetMask("PlaneLayerMask");

        // ������ �浹�� �����ϵ��� ����ĳ��Ʈ�� ����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    // �� �� ���̿� ť�긦 �����ϴ� �Լ�
    public void CreateCube(Vector3 start, Vector3 end, int obs_id)
    {
        // �������� �������� ���� �밢���� �� �������� ����
        Vector3 topLeft = new Vector3(Mathf.Min(start.x, end.x), Mathf.Max(start.y, end.y), Mathf.Min(start.z, end.z));
        Vector3 bottomRight = new Vector3(Mathf.Max(start.x, end.x), Mathf.Min(start.y, end.y), Mathf.Max(start.z, end.z));

        // �밢���� �߽����� ����
        Vector3 middlePoint = (topLeft + bottomRight) / 2;

        // ť�� ����
        Debug.Log("floor num >> " + current_floor.floor_num);
        float obstaclce_y = des_y[current_floor.floor_num - 1];
        Vector3 cubePosition = new Vector3(middlePoint.x, obstaclce_y, middlePoint.z); // y ��ǥ�� ����
        GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        Debug.Log("create obstacle >> " + cube.transform.position);
        cube.transform.localScale = new Vector3(Mathf.Abs(start.x - end.x), 5.123f, Mathf.Abs(start.z - end.z));

        Debug.Log("CubeCreator >> ť�� ���� ��ǥ (����): " + cubePosition);
        obs_type new_obs = new obs_type(obs_id, current_floor.floor_num, cube, start, end);
        Obstacles.Add(new_obs);
        webSocket.Connect();
        progress_st = 2;
        webSocket.Send($"{{\"progress_st\": \"{progress_st}\", \"obs_id\": \"{obs_id}\", \"floor\": \"{current_floor.floor_num}\",\"start_x\": \"{start.x}\", \"start_z\": \"{start.z}\", \"end_x\": \"{end.x}\", \"end_z\": \"{end.z}\"}}");
        update_obs_list();
    }


    public void OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("CubeCreator WebSocket connection closed.");
    }

    private void OnApplicationQuit()
    {
        // ���ø����̼� ���� �� WebSocket ���� ����
        if (webSocket != null && webSocket.IsAlive)
        {
            webSocket.Close();
        }
    }
}