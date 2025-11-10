using UnityEngine;

public class RaycastDebugger : MonoBehaviour
{
   [Header("References")]
    public Camera playerCamera;
    public float rayDistance = 100f;

    void Awake()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    /// <summary>
    /// 현재 카메라 중심에서 발사되는 레이 반환
    /// </summary>
    public Ray GetViewRay()
    {
        return playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }

    /// <summary>
    /// 디버그용 — 화면 중앙 방향을 그려줌
    /// </summary>
    public void DrawDebugRay(Color color, float duration = 0.05f)
    {
        Ray ray = GetViewRay();
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, color, duration);
    }
}
