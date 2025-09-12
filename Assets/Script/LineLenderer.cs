using UnityEngine;

// LineRenderer 공부하는 
public class LineDrawerScript : MonoBehaviour
{
    private LineRenderer lineRenderer;


    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.widthMultiplier = 0.3f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.red;

        Vector3[] points = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(100, 5, 0)
        };

        lineRenderer.positionCount = points.Length; // ❗ 꼭 필요
        lineRenderer.SetPositions(points);
    }
}
