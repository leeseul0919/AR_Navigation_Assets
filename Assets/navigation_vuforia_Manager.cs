using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class navigation_vuforia_Manager : MonoBehaviour
{
    public GameObject minimap; // �̴ϸ� GameObject
    public GameObject idpanel; // ID �г� GameObject
    public GameObject searchinput; // �˻� �Է� �ʵ� GameObject
    public GameObject searchbutton; // �˻� ��ư GameObject
    public GameObject firsttext;

    public CloudRecoBehaviour mCloudRecoBehaviour; // CloudRecoBehaviour ������Ʈ
    public int st1;
    bool mIsScanning = false;
    string mTargetMetadata = "";
    private void Start()
    {
        st1 = PlayerPrefs.GetInt("ST");
    }
    void Awake()
    {
        if (st1 == 2)
        {
            mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
            mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
            mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
            mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
            mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
            mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
            Debug.Log("Awake");
        }
    }

    void OnDestroy()
    {
        if (mCloudRecoBehaviour != null)
        {
            mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
            mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
            mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
            mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
            mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
        }
    }

    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        // �̹��� Ÿ���� �����Ǿ��� �� ȣ��˴ϴ�.
        if (cloudRecoSearchResult != null)
        {
            mTargetMetadata = cloudRecoSearchResult.MetaData;

            Debug.Log("Detected Image Target: " + cloudRecoSearchResult.TargetName);

            mCloudRecoBehaviour.enabled = false;
            Debug.Log("image found");
            firsttext.SetActive(false);
            StartNavigation();
        }
    }
    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());
    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        if (scanning)
        {
            // Clear all known targets
        }
    }
    void StartNavigation()
    {
        // �̹��� Ÿ���� �����Ǹ� ������ ������ �����մϴ�.
        // ���⿡�� ���ϴ� ������ �߰��Ͻø� �˴ϴ�.
        Debug.Log("Image target detected! Starting navigation...");

        minimap.SetActive(true);
        idpanel.SetActive(true);
        searchinput.SetActive(true);
        searchbutton.SetActive(true);
            
    }
}
