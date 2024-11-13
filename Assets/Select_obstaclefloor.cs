using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Select_obstaclefloor : MonoBehaviour
{
    public int floor_num;
    public GameObject Obstacle_Cube;
    public GameObject Plane_1F;
    public GameObject Plane_2F;
    public GameObject Plane_3F;
    public GameObject Plane_4F;
    public GameObject Plane_5F;
    public GameObject Plane_6F;

    public GameObject LayerMask_1F;
    public GameObject LayerMask_2F;
    public GameObject LayerMask_3F;
    public GameObject LayerMask_4F;
    public GameObject LayerMask_5F;
    public GameObject LayerMask_6F;

    private CubeCreator cubeCreator;

    private void Start()
    {
        floor_num = 0;
        cubeCreator = FindObjectOfType<CubeCreator>();
        if (cubeCreator == null)
        {
            Debug.LogError("CubeCreator instance not found!");
        }
    }

    public void select_1floor()
    {
        GameObject.Find("1F_Collection/1F_Image").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("2F_Collection/2F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("3F_Collection/3F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("4F_Collection/4F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("5F_Collection/5F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("6F_Collection/6F_Image").GetComponent<MeshRenderer>().enabled = false;

        LayerMask_1F.SetActive(true);
        LayerMask_2F.SetActive(false);
        LayerMask_3F.SetActive(false);
        LayerMask_4F.SetActive(false);
        LayerMask_5F.SetActive(false);
        LayerMask_6F.SetActive(false);

        Plane_1F = GameObject.Find("1F_Collection/1F_Image");
        Obstacle_Cube = GameObject.Find("Cube");
        Obstacle_Cube.transform.position = new Vector3(71.17f, Plane_1F.transform.position.y + 30f, 87.6f);
        floor_num = 1;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        Debug.Log("floor num >> " + floor_num);
        cubeCreator.UpdateObstacleVisibility(floor_num);
    }

    public void select_2floor()
    {
        GameObject.Find("1F_Collection/1F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("2F_Collection/2F_Image").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("3F_Collection/3F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("4F_Collection/4F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("5F_Collection/5F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("6F_Collection/6F_Image").GetComponent<MeshRenderer>().enabled = false;

        LayerMask_1F.SetActive(false);
        LayerMask_2F.SetActive(true);
        LayerMask_3F.SetActive(false);
        LayerMask_4F.SetActive(false);
        LayerMask_5F.SetActive(false);
        LayerMask_6F.SetActive(false);

        Plane_2F = GameObject.Find("2F_Collection/2F_Image");
        Obstacle_Cube = GameObject.Find("Cube");
        Obstacle_Cube.transform.position = new Vector3(71.17f, Plane_2F.transform.position.y + 30f, 87.6f);
        floor_num = 2;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        Debug.Log("floor num >> " + floor_num);
        cubeCreator.UpdateObstacleVisibility(floor_num);
    }

    public void select_3floor()
    {
        GameObject.Find("1F_Collection/1F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("2F_Collection/2F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("3F_Collection/3F_Image").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("4F_Collection/4F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("5F_Collection/5F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("6F_Collection/6F_Image").GetComponent<MeshRenderer>().enabled = false;

        LayerMask_1F.SetActive(false);
        LayerMask_2F.SetActive(false);
        LayerMask_3F.SetActive(true);
        LayerMask_4F.SetActive(false);
        LayerMask_5F.SetActive(false);
        LayerMask_6F.SetActive(false);

        Plane_3F = GameObject.Find("3F_Collection/3F_Image");
        Obstacle_Cube = GameObject.Find("Cube");
        Obstacle_Cube.transform.position = new Vector3(71.17f, Plane_3F.transform.position.y + 30f, 87.6f);
        floor_num = 3;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        Debug.Log("floor num >> " + floor_num);
        cubeCreator.UpdateObstacleVisibility(floor_num);
    }

    public void select_4floor()
    {
        GameObject.Find("1F_Collection/1F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("2F_Collection/2F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("3F_Collection/3F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("4F_Collection/4F_Image").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("5F_Collection/5F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("6F_Collection/6F_Image").GetComponent<MeshRenderer>().enabled = false;

        LayerMask_1F.SetActive(false);
        LayerMask_2F.SetActive(false);
        LayerMask_3F.SetActive(false);
        LayerMask_4F.SetActive(true);
        LayerMask_5F.SetActive(false);
        LayerMask_6F.SetActive(false);

        Plane_4F = GameObject.Find("4F_Collection/4F_Image");
        Obstacle_Cube = GameObject.Find("Cube");
        Obstacle_Cube.transform.position = new Vector3(71.17f, Plane_4F.transform.position.y + 30f, 87.6f);
        floor_num = 4;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        Debug.Log("floor num >> " + floor_num);
        cubeCreator.UpdateObstacleVisibility(floor_num);
    }

    public void select_5floor()
    {
        GameObject.Find("1F_Collection/1F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("2F_Collection/2F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("3F_Collection/3F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("4F_Collection/4F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("5F_Collection/5F_Image").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("6F_Collection/6F_Image").GetComponent<MeshRenderer>().enabled = false;

        LayerMask_1F.SetActive(false);
        LayerMask_2F.SetActive(false);
        LayerMask_3F.SetActive(false);
        LayerMask_4F.SetActive(false);
        LayerMask_5F.SetActive(true);
        LayerMask_6F.SetActive(false);

        Plane_5F = GameObject.Find("5F_Collection/5F_Image");
        Obstacle_Cube = GameObject.Find("Cube");
        Obstacle_Cube.transform.position = new Vector3(71.17f, Plane_5F.transform.position.y + 30f, 87.6f);
        floor_num = 5;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        Debug.Log("floor num >> " + floor_num);
        cubeCreator.UpdateObstacleVisibility(floor_num);
    }

    public void select_6floor()
    {
        GameObject.Find("1F_Collection/1F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("2F_Collection/2F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("3F_Collection/3F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("4F_Collection/4F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("5F_Collection/5F_Image").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("6F_Collection/6F_Image").GetComponent<MeshRenderer>().enabled = true;

        LayerMask_1F.SetActive(false);
        LayerMask_2F.SetActive(false);
        LayerMask_3F.SetActive(false);
        LayerMask_4F.SetActive(false);
        LayerMask_5F.SetActive(false);
        LayerMask_6F.SetActive(true);

        Plane_6F = GameObject.Find("6F_Collection/6F_Image");
        Obstacle_Cube = GameObject.Find("Cube");
        Obstacle_Cube.transform.position = new Vector3(71.17f, Plane_6F.transform.position.y + 30f, 87.6f);
        floor_num = 6;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        Debug.Log("floor num >> " + floor_num);
        cubeCreator.UpdateObstacleVisibility(floor_num);
    }
}
