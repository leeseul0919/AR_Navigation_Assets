using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;
using UnityEngine.XR;

public class EV_Use_Manager : MonoBehaviour
{
    public GameObject User_Object;
    public InputField select_floor1;
    public InputField select_floor2;
    public GameObject guide_text;
    public int floor;
    public bool iselevator;

    public float[] set_y;
    public GameObject ev1_position;
    public GameObject ev2_position;
    public GameObject XR_Origin_position;
    private void Start()
    {
        iselevator = false;
        set_y = new float[] { 0.3f, 6.48f, 12.66f, 18.84f, 25.02f, 31.2f };
    }
    public void enter_EV1()
    {
        select_floor1 = GameObject.Find("Canvas/EV1_Function/EV1_Floor_Input").GetComponent<InputField>();
        floor = int.Parse(select_floor1.text);
        Debug.Log(floor);
        GameObject.Find("Canvas/EV1_Function/EV_Guide").SetActive(true);
        GameObject.Find("Canvas/EV1_Function/EV1_Arrive_Button").SetActive(true);
        GameObject.Find("Canvas/EV1_Function/EV1_Enter_Button").SetActive(false);
        iselevator = true;
    }
    public void arrive_EV1()
    {
        Vector3 gap = XR_Origin_position.transform.position - User_Object.transform.position;
        XR_Origin_position.transform.position = new Vector3(ev1_position.transform.position.x, set_y[floor - 1], ev1_position.transform.position.z);
        XR_Origin_position.transform.position += gap;
        User_Object.transform.eulerAngles = new Vector3(0, 0, 0);
        GameObject.Find("Canvas/EV1_Function/EV_Guide").SetActive(false);
        GameObject.Find("Canvas/EV1_Function/EV1_Arrive_Button").SetActive(false);
        GameObject.Find("Canvas/EV1_Function/EV1_Enter_Button").SetActive(true);
        iselevator = false;
    }
    public void enter_EV2()
    {
        select_floor2 = GameObject.Find("Canvas/EV2_Function/EV2_Floor_Input").GetComponent<InputField>();
        floor = int.Parse(select_floor2.text);
        Debug.Log(floor);
        GameObject.Find("Canvas/EV2_Function/EV_Guide").SetActive(true);
        GameObject.Find("Canvas/EV2_Function/EV2_Arrive_Button").SetActive(true);
        GameObject.Find("Canvas/EV2_Function/EV2_Enter_Button").SetActive(false);
        iselevator = true;
    }
    public void arrive_EV2()
    {
        Vector3 gap = XR_Origin_position.transform.position - User_Object.transform.position;
        XR_Origin_position.transform.position = new Vector3(ev2_position.transform.position.x, set_y[floor - 1], ev2_position.transform.position.z);
        XR_Origin_position.transform.position += gap;
        User_Object.transform.eulerAngles = new Vector3(0, 90, 0);
        GameObject.Find("Canvas/EV2_Function/EV_Guide").SetActive(false);
        GameObject.Find("Canvas/EV2_Function/EV2_Arrive_Button").SetActive(false);
        GameObject.Find("Canvas/EV2_Function/EV2_Enter_Button").SetActive(true);
        iselevator = false;
    }
}
