using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using UnityEngine.UI;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class obstacle_state_Manager : MonoBehaviour
{
    public string mongodbURI = "mongodb://OS:MZWl4yS6ylx53ouQ@atlas-sql-6560a86c6cf7c44c1c5f5f3c-tbnaj.a.query.mongodb.net/test?ssl=true&authSource=admin";
    public string dbName = "test";
    public string collectionName = "obstacles";

    public MongoClient client;
    public IMongoDatabase database;
    public IMongoCollection<BsonDocument> collection;

    public GameObject cubePrefab;

    public CubeCreator obstacle_list;
    public float[] des_y;
    void Start()
    {
        des_y = new float[] { 0.3f, 6.48f, 12.66f, 18.84f, 25.02f, 31.2f };

        client = new MongoClient(mongodbURI);
        database = client.GetDatabase(dbName);
        collection = database.GetCollection<BsonDocument>(collectionName);

        var filter = Builders<BsonDocument>.Filter.Empty;
        var cursor = collection.FindSync(filter);
        var documents = cursor.ToList();
        
        // ������ ������ ó��
        foreach (var doc in documents)
        {
            // ������ ó�� �ڵ� �ۼ�
            int obsId = doc["obs_id"].AsInt32;
            int floor_n = doc["floor"].AsInt32;
            double startX = doc["start_x"].AsDouble;
            //double startY = 0.1; // ���� ����
            double startZ = doc["start_z"].AsDouble;
            double endX = doc["end_x"].AsDouble;
            //double endY = 2.0; // ���� ����
            double endZ = doc["end_z"].AsDouble;

            Vector3 start = new Vector3((float)startX, 0, (float)startZ);
            Vector3 end = new Vector3((float)endX, 0, (float)endZ);
            CreateCube(start, end, floor_n, obsId);
        }
        obstacle_list.update_obs_list();
    }
    public void CreateCube(Vector3 start, Vector3 end, int floor_num, int obs_id)
    {
        // �������� �������� ���� �밢���� �� �������� ����
        Vector3 topLeft = new Vector3(Mathf.Min(start.x, end.x), Mathf.Max(start.y, end.y), Mathf.Min(start.z, end.z));
        Vector3 bottomRight = new Vector3(Mathf.Max(start.x, end.x), Mathf.Min(start.y, end.y), Mathf.Max(start.z, end.z));

        // �밢���� �߽����� ����
        Vector3 middlePoint = (topLeft + bottomRight) / 2;

        // ť�� ����
        Vector3 cubePosition = new Vector3(middlePoint.x, des_y[floor_num-1], middlePoint.z); // y ��ǥ�� ����
        GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
        cube.transform.localScale = new Vector3(Mathf.Abs(start.x - end.x), 5.123f, Mathf.Abs(start.z - end.z));

        Debug.Log("obstacle_state_Manage >> ť�� ���� ��ǥ (����): " + cubePosition);
        obs_type new_obs = new obs_type(obs_id, floor_num, cube, start, end);
        obstacle_list.Obstacles.Add(new_obs);
    }
}
