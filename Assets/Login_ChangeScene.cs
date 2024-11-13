using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using TMPro;

public class Login_ChangeScene : MonoBehaviour
{

    public MongoClient client;
    public IMongoDatabase userDatabase;
    public IMongoCollection<BsonDocument> userCollection;

    private const string USER_MONGODB_URI_FORMAT = "mongodb://OS:MZWl4yS6ylx53ouQ@atlas-sql-6560a86c6cf7c44c1c5f5f3c-tbnaj.a.query.mongodb.net/test?ssl=true&authSource=admin";
    private const string USER_DATABASE_NAME = "test";
    private const string USER_COLLECTION_NAME = "users";

    public InputField idInputField;
    public InputField passwordInputField;
    public string nickname;
    public string password;
    public TextMeshProUGUI messageText;
    public void Change()
    {
        // MongoDB ���� ����
        client = new MongoClient(USER_MONGODB_URI_FORMAT);
        userDatabase = client.GetDatabase(USER_DATABASE_NAME);
        userCollection = userDatabase.GetCollection<BsonDocument>(USER_COLLECTION_NAME);

        string nickname = idInputField.text;
        string password = passwordInputField.text;

        // MongoDB���� ����� ���� Ȯ��
        var filter = Builders<BsonDocument>.Filter.Eq("ID", nickname) & Builders<BsonDocument>.Filter.Eq("Password", password);
        var result = userCollection.Find(filter).FirstOrDefault();
        Debug.Log(result);
        Debug.Log(userCollection.Find(filter).FirstOrDefault());

        if (result != null)
        {
            // Manager_check �ʵ��� ���� ���������� Ȯ���ϰ� ������ ��ȯ
            int managerCheck = result.GetValue("Manager_check").AsInt32;

            // �α��� ���� �� ���� ������ �̵�
            PlayerPrefs.SetString("Nick", nickname);
            PlayerPrefs.SetInt("ManagerCheck", managerCheck); // Manager_check �����͸� PlayerPrefs�� ����
            Debug.Log("�α��� ����! ����� �̸�: " + nickname);
            SceneManager.LoadScene("menu");
        }
        else
        {
            // �α��� ���� �� �޽��� ���
            Debug.Log("�α��� ����: ����� �̸� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            messageText.text = "�α��� ����: ����� �̸� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            StartCoroutine(ClearMessageText(2f));
        }


        GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text = "";
        GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text = "";

        // MongoDB Ŭ���̾�Ʈ ����
        client = null;
        userDatabase = null;
        userCollection = null;
    }

    IEnumerator ClearMessageText(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }

}