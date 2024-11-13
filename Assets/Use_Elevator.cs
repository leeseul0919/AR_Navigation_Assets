using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Elevator : MonoBehaviour
{
    public GameObject Elevator_Function1;
    public GameObject Elevator_Function2;
    public GameObject User_Object;

    // ���������Ͱ� Ȱ��ȭ�Ǵ� ��ġ
    private Vector3 targetPosition1;
    private Vector3 targetPosition2;
    public GameObject ev1_position;
    public GameObject ev2_position;
    public float activationRadius;

    public EV_Use_Manager user_iselevator;

    private void Start()
    {
        Debug.Log("User EV Start");
        activationRadius = 2.0f;
        targetPosition1 = ev1_position.transform.position;
        targetPosition2 = ev2_position.transform.position;
    }

    void Update()
    {
        Vector3 userPosition = User_Object.transform.position;

        // User_Object�� ��ġ�� targetPosition ���� �Ÿ� ���
        float distance1 = Vector3.Distance(userPosition, targetPosition1);
        float distance2 = Vector3.Distance(userPosition, targetPosition2);

        // �Ÿ��� ���� ���� �ִ��� Ȯ��
        if (distance1 <= activationRadius)
        {
            if(user_iselevator.iselevator == false)
            {
                // Elevator_Function Ȱ��ȭ
                Elevator_Function1.SetActive(true);
                Elevator_Function2.SetActive(false);
            }
            
        }
        else if(distance2 <= activationRadius)
        {
            if (user_iselevator.iselevator == false)
            {
                // Elevator_Function Ȱ��ȭ
                Elevator_Function1.SetActive(false);
                Elevator_Function2.SetActive(true);
            }
        }
        else
        {
            if (user_iselevator.iselevator == false)
            {
                // Elevator_Function ��Ȱ��ȭ
                Elevator_Function1.SetActive(false);
                Elevator_Function2.SetActive(false);
            }
        }
    }
}
