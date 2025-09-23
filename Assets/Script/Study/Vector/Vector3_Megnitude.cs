using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3_Megnitude : MonoBehaviour
{
    // 필드의 초기화
    //*****************************************************************************************************************
    // Vector3 v = new Vector3(3, 4, 0);
    // float length = v.magnitude;
    //*****************************************************************************************************************

    Vector3 v = new Vector3(3, 4, 0);
    void Start()
    {
        float length = v.magnitude;
        Debug.Log("백터의 길이 : " + length);
    }
}
