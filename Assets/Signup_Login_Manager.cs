using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using WebSocketSharp;

public class Signup_Login_Manager : MonoBehaviour
{
    public string nickname;
    public string password;

    // ���� ����
    public InputField idInputField;
    public InputField passwordInputField;

    public GameObject gobackLogin;
    public GameObject loginButton;
    public GameObject goSignUpButton;
    public GameObject signupButton;
    public GameObject cteatetext;
    public TextMeshProUGUI messageText;

    WebSocket webSocket = new WebSocket("wss://port-0-docker-node-be-1ru12mlvsjr55i.sel5.cloudtype.app/:3000");
    public bool existingUser = false;
    public bool signupSuccess = false;
    public bool loginSuccess = false;
    public bool received_message = false;
    public int try_signup_login = 0;
    public bool manager_check = false;
    public void Start()
    {
        loginButton = GameObject.Find("LoginCanvas/LoginButton");
        goSignUpButton = GameObject.Find("LoginCanvas/SignUp/goSignUpButton");
        signupButton = GameObject.Find("LoginCanvas/SignUp/SignUpButton");
        cteatetext = GameObject.Find("LoginCanvas/SignUp/cteatetext");
        gobackLogin = GameObject.Find("LoginCanvas/gotoLogin");

        loginButton.SetActive(true);
        goSignUpButton.SetActive(true);
        signupButton.SetActive(false);
        cteatetext.SetActive(false);
        gobackLogin.SetActive(false);

        existingUser = false;
        signupSuccess = false;
        loginSuccess = false;
        received_message = false;
        try_signup_login = 0;
        manager_check = false;
    }

    public void goSignUp()
    {
        GameObject.Find("LoginCanvas/LoginButton").SetActive(false);
        GameObject.Find("LoginCanvas/SignUp/goSignUpButton").SetActive(false);
        GameObject.Find("LoginCanvas/SignUp/SignUpButton").SetActive(true);
        GameObject.Find("LoginCanvas/SignUp/cteatetext").SetActive(true);
        GameObject.Find("LoginCanvas/gotoLogin").SetActive(true);
        // �Է� �ʵ� �ʱ�ȭ
        GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text = "";
        GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text = "";

    }
    public void goback_Login()
    {
        GameObject.Find("LoginCanvas/LoginButton").SetActive(true);
        GameObject.Find("LoginCanvas/SignUp/goSignUpButton").SetActive(true);
        GameObject.Find("LoginCanvas/SignUp/SignUpButton").SetActive(false);
        GameObject.Find("LoginCanvas/SignUp/cteatetext").SetActive(false);
        GameObject.Find("LoginCanvas/gotoLogin").SetActive(false);
        // �Է� �ʵ� �ʱ�ȭ
        GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text = "";
        GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text = "";
    }
    public void SignUp()
    {
        webSocket.OnMessage += OnMessage;
        webSocket.OnClose += OnClose;
        webSocket.Connect();
        Debug.Log(webSocket);
        try_signup_login = 1;
        received_message = false;
        nickname = GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text;
        password = GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text;
        Debug.Log("enter signup");
        int progress_st = 1;
        webSocket.Send($"{{\"progress_st\": \"{progress_st}\",\"nickname\": \"{nickname}\", \"password\": \"{password}\"}}");
        Debug.Log("send data");
        Debug.Log(webSocket);

        StartCoroutine(WaitForSignupResult(nickname));
    }
    public void Login_send()
    {
        webSocket.OnMessage += OnMessage;
        webSocket.OnClose += OnClose;
        webSocket.Connect();
        Debug.Log(webSocket);
        try_signup_login = 2;
        received_message = false;
        nickname = GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text;
        password = GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text;
        Debug.Log("enter login");
        Debug.Log(nickname + " " + password + " " + webSocket);
        int progress_st = 4;
        webSocket.Send($"{{\"progress_st\": \"{progress_st}\",\"nickname\": \"{nickname}\", \"password\": \"{password}\"}}");
        Debug.Log("send data");
        Debug.Log(webSocket);

        StartCoroutine(WaitForSignupResult(nickname));
    }

private IEnumerator WaitForSignupResult(string nickname)
    {
        Debug.Log("message");
        // �޽����� ���� ������ ���
        while (!received_message)
        {
            yield return null;
        }
        Debug.Log("message receive");
        received_message = false;
        // ȸ������ ���� ��
        if (signupSuccess==true && try_signup_login == 1)
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
        else if(signupSuccess==false && try_signup_login == 1)
        {
            Debug.Log("�̹� ��ϵ� ������Դϴ�.");
            messageText.text = "�̹� ��ϵ� ������Դϴ�.";
            StartCoroutine(ClearMessageText(2f));
        }
        else if (loginSuccess == true && try_signup_login == 2)
        {
            int managerCheck = 0;
            if (manager_check) managerCheck = 1;

            PlayerPrefs.SetString("Nick", nickname);
            PlayerPrefs.SetInt("ManagerCheck", managerCheck); // Manager_check �����͸� PlayerPrefs�� ����
            Debug.Log("�α��� ����! ����� �̸�: " + nickname);
            SceneManager.LoadScene("menu");
            StartCoroutine(ClearMessageText(2f));
        }
        else if (loginSuccess == false && try_signup_login == 2)
        {
            Debug.Log("�α��� ����: ����� �̸� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            messageText.text = "�α��� ����: ����� �̸� �Ǵ� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            StartCoroutine(ClearMessageText(2f));
        }
        GameObject.Find("LoginCanvas/InputFieldID").GetComponent<InputField>().text = "";
        GameObject.Find("LoginCanvas/InputFieldPS").GetComponent<InputField>().text = "";
    }
    public void OnMessage(object sender, MessageEventArgs e)
    {
        // ���� �޽����� Ȯ���Ͽ� ó��
        received_message = true;
        int response = int.Parse(e.Data);
        if (response == 1)
        {
            Debug.Log("ȸ������ ����");
            signupSuccess = true;
        }
        else if (response == 2)
        {
            Debug.Log("ȸ������ ����");
            signupSuccess = false;
        }
        else if(response == 3)
        {
            Debug.Log("�α��� ����");
            loginSuccess = true;
        }
        else if (response == 4)
        {
            Debug.Log("�α��� ����");
            loginSuccess = true;
            manager_check = true;
        }
        else if(response == 5)
        {
            Debug.Log("�α��� ����");
            loginSuccess = false;
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
