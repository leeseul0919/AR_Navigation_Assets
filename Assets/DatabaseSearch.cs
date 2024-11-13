using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Text 사용을 위한 네임스페이스
// TextMeshPro 사용 시 아래 라인도 추가
// using TMPro;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine.AI;
using TMPro;

public class DatabaseSearch : MonoBehaviour
{
    public InputField searchInputField;
    public Button searchButton;
    public GameObject planeObject;
    public GameObject cubePrefab;
    public TMP_Text floorText; // UI 텍스트 참조 추가 (TextMeshPro 사용 시 TMP_Text로 변경)
    string searchText;
    public static float node_x;
    public static float node_y;
    public static float scale_x;
    public static float scale_y;
    public static float plane_w;
    public static float plane_h;
    private TargetChanger targetChanger;
    public Camera otherCamera;
    public const string MONGODB_URI_FORMAT = "mongodb://OS:MZWl4yS6ylx53ouQ@atlas-sql-6560a86c6cf7c44c1c5f5f3c-tbnaj.a.query.mongodb.net/test?ssl=true&authSource=admin";
    private const string TEST_DB = "test";
    private const string TEST_COLLECTION = "destination_position";
    public string collectionName = "obstacles";
    public int des_ID;

    public MongoClient client;
    public IMongoDatabase database;
    public IMongoCollection<TestData> testCollection;

    public GameObject real_player_ar;
    public NavMeshAgent target_player;

    public ListDatabaseText textarray_object;
    public float des_x;
    public float des_y;

    private void Start()
    {
        searchText = null;
        GetImageSize();
        targetChanger = FindObjectOfType<TargetChanger>();
        otherCamera = GameObject.Find("ScaleCamera").GetComponent<Camera>();
        if (otherCamera == null)
        {
            Debug.LogError("Other camera not found!");
        }

        client = new MongoClient(MONGODB_URI_FORMAT);
        database = client.GetDatabase(TEST_DB);
        testCollection = database.GetCollection<TestData>(TEST_COLLECTION);

        var filter = Builders<TestData>.Filter.Empty;
        var result = testCollection.Find(filter).ToList();
        foreach (var data in result)
        {
            textarray_object.des_name_list.Add(data.Text);
        }
        floorText = GameObject.Find("Canvas/FloorText").GetComponent<TMP_Text>();
    }

    public DatabaseSearch set_databasesearch()
    {
        return this;
    }

    public int floor;

    public void SearchDatabase()
    {
        searchText = searchInputField.text;
        Debug.Log(searchText);
        Debug.Log("Got Client");
        Debug.Log("good");

        try
        {
            var filter = Builders<TestData>.Filter.Regex("Text", new BsonRegularExpression(searchText, "i"));
            var result = testCollection.Find(filter).ToList();

            if (result.Count > 0)
            {
                float xx = 0, yy = 0;
                int res_des_ID = 0;
                int db_floor = 0;
                foreach (var document in result)
                {
                    Debug.Log("검색 결과: " + document.ID + "->" + document.Text + " (" + document.Node_X + ", " + document.Node_Y + ")");
                    res_des_ID = document.ID;
                    xx = document.Node_X;
                    yy = document.Node_Y;
                    db_floor = document.floor;
                }
                des_ID = res_des_ID;
                node_x = xx;
                node_y = yy;
                floor = db_floor;
                set_destination_pos();

                // 검색 결과의 floor 값을 UI 텍스트에 표시
                floorText.text = "Destination Floor: " + floor.ToString();
            }
            else
            {
                Debug.Log("목적지를 다시 입력해주세요.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("MongoDB 연결 또는 검색 실패: " + ex.Message);
        }
    }

    void GetImageSize()
    {
        Renderer renderer = planeObject.GetComponent<Renderer>();
        Texture texture = renderer.material.mainTexture;
        if (texture is Texture2D)
        {
            Texture2D texture2D = (Texture2D)texture;
            int imageWidth = texture2D.width;
            int imageHeight = texture2D.height;
            float planeWidth = renderer.bounds.size.x;
            float planeHeight = renderer.bounds.size.z;
            plane_w = planeWidth;
            plane_h = planeHeight;
            Debug.Log("이미지: (" + imageWidth + "," + imageHeight + ")");
            Debug.Log("플레인: (" + planeWidth + "," + planeHeight + ")");

            scale_x = planeWidth / imageWidth;
            scale_y = planeHeight / imageHeight;

            Debug.Log("스케일 가로: " + scale_x + ", 스케일 세로: " + scale_y);
        }
        else
        {
            Debug.LogError("사진X");
        }
    }

    void set_destination_pos()
    {
        float clone_x = node_x;
        float clone_y = node_y;

        float plane_pos_x = clone_x * scale_x;
        float plane_pos_y = clone_y * scale_y;

        plane_pos_x = plane_w - plane_pos_x;

        des_x = plane_pos_x;
        des_y = plane_pos_y;

        Debug.Log("plane_pos_x: " + plane_pos_x + ", plane_pos_y: " + plane_pos_y);
        targetChanger.destination_pos(1, plane_pos_x, plane_pos_y, floor);
    }
}
