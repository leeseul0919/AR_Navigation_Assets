using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class obstacle_camera_Manager : MonoBehaviour
{
    public float moveSpeed = 100.0f; // Cube의 이동 속도
    public Button forwardButton;
    public Button backwardButton;
    public Button rightButton;
    public Button leftButton;
    public Button zoomInButton;
    public Button zoomOutButton;
    public GameObject cubeObject;

    public bool IS_FORWARD;
    public bool IS_BACKWARD;
    public bool IS_RIGHT;
    public bool IS_LEFT;
    public bool IS_ZOOM_IN;
    public bool IS_ZOOM_OUT;
    public void Start()
    {
        forwardButton = GameObject.Find("Canvas/UpButton").GetComponent<Button>();
        backwardButton = GameObject.Find("Canvas/DownButton").GetComponent<Button>();
        leftButton = GameObject.Find("Canvas/LeftButton").GetComponent<Button>();
        rightButton = GameObject.Find("Canvas/RightButton").GetComponent<Button>();
        zoomInButton = GameObject.Find("Canvas/ExtensionButton").GetComponent<Button>();
        zoomOutButton = GameObject.Find("Canvas/ReductionButton").GetComponent<Button>();
        cubeObject = GameObject.Find("Cube");
        IS_FORWARD = false;
        IS_BACKWARD = false;
        IS_RIGHT = false;
        IS_LEFT = false;
        IS_ZOOM_IN = false;
        IS_ZOOM_OUT = false;
    }
    
    public void FORWARD_DOWN()
    {
        IS_FORWARD = true;
        Debug.Log("Forward down");
        Debug.Log(IS_FORWARD);
        GameObject.Find("Cube").transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
    public void FORWARD_UP()
    {
        IS_FORWARD = false;
        Debug.Log("Forward up");
        Debug.Log(IS_FORWARD);
    }
    public void BACKWARD_DOWN()
    {
        IS_BACKWARD = true;
        GameObject.Find("Cube").transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    public void BACKWARD_UP()
    {
        IS_BACKWARD = false;
    }
    public void RIGHT_DOWN()
    {
        IS_RIGHT = true;
        GameObject.Find("Cube").transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    public void RIGHT_UP()
    {
        IS_RIGHT = false;
    }
    public void LEFT_DOWN()
    {
        IS_LEFT = true;
        GameObject.Find("Cube").transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
    public void LEFT_UP()
    {
        IS_LEFT = false;
    }



    public void ZOOMIN_DOWN()
    {
        IS_ZOOM_IN = true;
        GameObject.Find("Cube").transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
    public void ZOOMIN_UP()
    {
        IS_ZOOM_IN = false;
    }



    public void ZOOMOUT_DOWN()
    {
        IS_ZOOM_OUT = true;
        GameObject.Find("Cube").transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    public void ZOOMOUT_UP()
    {
        IS_ZOOM_OUT = false;
    }
}
