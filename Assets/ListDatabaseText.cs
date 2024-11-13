using UnityEngine;
using UnityEngine.UI;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TMPro;

public class ListDatabaseText : MonoBehaviour
{
    public InputField searchInputField;
    public GameObject resultPrefab;
    public Transform contentTransform;

    public List<string> des_name_list = new List<string>();

    private void Start()
    {
        searchInputField.onValueChanged.AddListener(delegate { OnSearchInputValueChanged(); });
    }

    // �Է� �ʵ� ���� ����� ������ ȣ��Ǵ� �̺�Ʈ �ڵ鷯
    private void OnSearchInputValueChanged()
    {
        string searchText = searchInputField.text;

        // �˻�� ��� �ִ� ��� ��� �����͸� �ҷ����� �ʵ��� ����
        if (string.IsNullOrEmpty(searchText))
        {
            ClearContent();
        }
        else
        {
            SearchDatabaseList(searchText);
        }
    }

    private void ClearContent()
    {
        // Clear content
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SearchDatabaseList(string searchText)
    {
        // Clear previous results
        ClearContent();

        List<string> result = new List<string>();
        foreach(string item in des_name_list)
        {
            if (item.Contains(searchText)) result.Add(item);
        }

        if (result.Count > 0)
        {
            foreach (string document in result)
            {
                // Instantiate�ϱ� ���� �ı��Ǿ����� Ȯ��
                if (contentTransform == null) return;

                GameObject resultItem = Instantiate(resultPrefab, contentTransform);

                // resultItem�� null�� �ƴ��� Ȯ��
                if (resultItem != null)
                {
                    // TextMeshPro �ؽ�Ʈ ����
                    TextMeshProUGUI textMeshPro = resultItem.GetComponentInChildren<TextMeshProUGUI>();
                    if (textMeshPro != null)
                    {
                        textMeshPro.text = document;
                    }
                    else
                    {
                        Debug.LogError("TextMeshPro component not found in the resultPrefab.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to instantiate resultPrefab.");
                }
            }
        }
        else
        {
            Debug.Log("No results found.");
        }
    }
}
