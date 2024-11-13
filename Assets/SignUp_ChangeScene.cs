using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using WebSocketSharp;

public class SignUp_ChangeScene : MonoBehaviour
{
    public string nickname;
    public string password;

    // ���� ����
    public InputField idInputField;
    public InputField passwordInputField;

    public GameObject loginButton;
    public GameObject goSignUpButton;
    public GameObject signupButton;
    public GameObject cteatetext;
    public TextMeshProUGUI messageText;

    public WebSocket webSocket;
    public bool existingUser = false;
    public bool signupSuccess = false;
    public void Start()
    {
        webSocket = new WebSocket("wss://port-0-docker-node-be-1ru12mlvsjr55i.sel5.cloudtype.app/:3000");
        webSocket.OnMessage += OnMessage;
        webSocket.OnClose += OnClose;
        webSocket.Connect();
    }
    public void goSignUp()
    {
        loginButton.SetActive(false);
        goSignUpButton.SetActive(false);
        signupButton.SetActive(true);
        cteatetext.SetActive(true);
        // �Է� �ʵ� �ʱ�ȭ
        GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text = "";
        GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text = "";

    }
    public void SignUp()
    {
        nickname = GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text;
        password = GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text;
        Debug.Log("enter signup");
        int progress_st = 1;
        webSocket.Send($"{{\"progress_st\": \"{progress_st}\",\"nickname\": \"{nickname}\", \"password\": \"{password}\"}}");
        Debug.Log("send data");
        Debug.Log(webSocket);

        StartCoroutine(WaitForSignupResult(nickname));
    }
    private IEnumerator WaitForSignupResult(string nickname)
    {
        Debug.Log("message");
        // �޽����� ���� ������ ���
        while (!signupSuccess)
        {
            yield return null;
        }
        Debug.Log("message receive");
        // ȸ������ ���� ��
        if (signupSuccess)
        {
            Debug.Log("ȸ������ ����! ����� �̸�: " + nickname);
            messageText.text = "ȸ������ ����!";
            StartCoroutine(ClearMessageText(2f));
            // UI ������Ʈ
            signupButton.SetActive(false);
            cteatetext.SetActive(false);
            loginButton.SetActive(true);
            goSignUpButton.SetActive(true);
        }
        // �̹� ��ϵ� ������� ���
        else
        {
            Debug.Log("�̹� ��ϵ� ������Դϴ�.");
            messageText.text = "�̹� ��ϵ� ������Դϴ�.";
            StartCoroutine(ClearMessageText(2f));
        }
        GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text = "";
        GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text = "";
    }
    public void OnMessage(object sender, MessageEventArgs e)
    {
        // ���� �޽����� Ȯ���Ͽ� ó��
        int response = int.Parse(e.Data);
        if (response == 1)
        {
            Debug.Log("�α��� ����");
            signupSuccess = true;
        }
        else if (response == 2)
        {
            Debug.Log("�α��� ����");
            signupSuccess = false;
        }
    }
    public void OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connection closed.");
    }
    private void OnApplicationQuit()
    {
        // ���ø����̼� ���� �� WebSocket ���� ����
        if (webSocket != null && webSocket.IsAlive)
        {
            webSocket.Close();
        }
    }
    IEnumerator ClearMessageText(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}
