using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCurve : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform start;
    public Transform Middle;
    public Transform end;
    public int segmentCount = 20;

    public Vector3[] vtarr;

    void Start()
    {
        vtarr = new Vector3[segmentCount];
        lineRenderer.positionCount = segmentCount;
    }

    void Update()
    {
        int half = segmentCount / 2;
        for (int i = 0; i < half; i++)
        {
            float t = (float)i / (half - 1); // start→middle
            vtarr[i] = Vector3.Lerp(start.position, Middle.position, t);

            t = (float)i / (half - 1);       // middle→end
            vtarr[i + half] = Vector3.Lerp(Middle.position, end.position, t);
        }

        lineRenderer.SetPositions(vtarr);
    }
}
