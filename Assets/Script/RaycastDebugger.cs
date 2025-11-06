using UnityEngine;

public class RaycastDebugger : MonoBehaviour
{
    [Header("Settings")]
    public KeyCode testKey = KeyCode.Mouse1;
    public LayerMask targetLayer;
    public float rayDistance = 100f;
    
    [Header("Camera")]
    public Camera playerCamera;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKey(testKey))
        {
            TestRaycast();
        }
    }

    void TestRaycast()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        // 레이 시각화
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow);
        
        Debug.Log("=== 레이캐스트 테스트 ===");
        Debug.Log("레이 시작점: " + ray.origin);
        Debug.Log("레이 방향: " + ray.direction);
        
        // 모든 충돌 체크
        RaycastHit[] hits = Physics.RaycastAll(ray, rayDistance, targetLayer);
        
        if (hits.Length == 0)
        {
            Debug.Log("❌ 아무것도 충돌 안 함!");
        }
        else
        {
            Debug.Log("✅ 총 " + hits.Length + "개 충돌!");
            
            // 모든 충돌 출력
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Debug.Log($"[{i}] 오브젝트: {hit.collider.gameObject.name}");
                Debug.Log($"    - 거리: {hit.distance:F2}m");
                Debug.Log($"    - 위치: {hit.point}");
                Debug.Log($"    - 레이어: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
                Debug.Log($"    - 태그: {hit.collider.gameObject.tag}");
                
                // Scene 뷰에 포인트 표시
                Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
                Debug.DrawRay(hit.point, hit.normal, Color.blue, 2f);
            }
        }
        
        Debug.Log("========================");
    }
}