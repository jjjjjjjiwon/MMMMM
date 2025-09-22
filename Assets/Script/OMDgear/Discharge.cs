using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Discharge : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool shoot;
    private float linesize = 15f;
    private float currentposition = 0.0f;
    private RaycastHit hit;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.widthMultiplier = 0.2f;
        Debug.Log("LineRenderer 준비 완료!");
        Debug.Log(transform.position.magnitude);
    }

    void Update()
    {
        lineRenderer.enabled = true;
        Vector3 lineStart = transform.position;
        Vector3 lineEnd = transform.position + transform.forward * currentposition;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot = !shoot;
        }
        if (shoot == true)
        {
            if (currentposition <= linesize)
            {
                currentposition += 0.1f;
                lineRenderer.SetPosition(0, lineStart);
                lineRenderer.SetPosition(1, lineEnd);
                Debug.Log("Space bar");
            }
        }
        else
        {
            if (currentposition > 0)
            {
                currentposition -= 0.2f; // 발사보다 빠르게 회수
                lineRenderer.SetPosition(0, lineStart);
                lineRenderer.SetPosition(1, lineEnd);

                Debug.Log("Space bar2");
            }
        }

    }

}
