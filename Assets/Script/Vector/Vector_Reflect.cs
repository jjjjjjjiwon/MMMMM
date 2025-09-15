using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

// Reflect : (입사 벡터, 법선 벡터) = 반사벡터
public class Vector_Reflect : MonoBehaviour
{
    Vector3 vt = new Vector3(1, 1, 0);
    Vector3 vtup = Vector3.up;
    Vector3 reflected;

    public void Start()
    {
        reflected = Vector3.Reflect(vt, vtup);
        Debug.Log("입사 벡터: " + vt);
        Debug.Log("법선 벡터: " + vtup);
        Debug.Log("반사 벡터: " + reflected);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, vt); // 입사 벡터

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, vtup); // 법선

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, reflected); // 반사 벡터
    }
}
