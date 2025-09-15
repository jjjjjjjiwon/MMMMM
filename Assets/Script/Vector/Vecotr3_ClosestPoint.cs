using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Vector3_ClosestPoint : MonoBehaviour
{
    public Collider co;
    public GameObject marker; // 작은 Sphere
    public Vector3 testPoint = new Vector3(1, 2, 3);

    void Start()
    {
        if (co != null)
        {
            Vector3 closest = co.ClosestPoint(testPoint);

            Debug.Log("원래 점: " + testPoint);
            Debug.Log("Collider에 가장 가까운 점: " + closest);
        }
    }

    void Update()
    {
        if (co != null && marker != null)
        {
            marker.transform.position = co.ClosestPoint(testPoint);
        }
    }
}
