using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRolling : MonoBehaviour
{
    [Header("Rolling Settings")]
    public float rollForce = 30f;       // 추진력
    public float rollDuration = 1f;     // 롤링 지속 시간
    public float rollSpinSpeed = 1080f; // 시각 회전 속도
    public float stopDistance = 5f;     // 그래플 충돌까지 최소 거리

    public Transform modelTransform;      // 시각적 모델 (회전용)
    public PlayerGrapple playerGrapple;   // 그래플 상태 참조

    private Rigidbody rb;
    private bool isRolling = false;

    public bool IsRolling => isRolling;   // 외부에서 체크 가능

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 롤링 시작 조건: E키 & 그래플 중 & 현재 롤링 아님
        if (Input.GetKeyDown(KeyCode.E) && !isRolling && playerGrapple != null && playerGrapple.IsGrappling)
        {
            StartCoroutine(DoRoll());
        }
    }

    private IEnumerator DoRoll()
    {
        isRolling = true;

        // 롤링 방향 결정
        Vector3 rollDir = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : transform.forward;

        // 루트 회전 고정
        Quaternion originalRotation = transform.rotation;

        // 모델 회전 초기화 (선택적)
        Quaternion originalModelRotation = modelTransform != null ? modelTransform.rotation : Quaternion.identity;

        float timer = 0f;

        while (playerGrapple != null && playerGrapple.IsGrappling && playerGrapple.springStrength > stopDistance && timer < rollDuration)
        {
            // 이동
            rb.AddForce(rollDir * rollForce, ForceMode.Acceleration);

            // 모델 회전 (시각적 회전)
            if (modelTransform != null)
                modelTransform.Rotate(Vector3.down * rollSpinSpeed * Time.deltaTime, Space.Self);

            // 루트 회전 고정
            transform.rotation = originalRotation;

            timer += Time.deltaTime;
            yield return null;
        }

        isRolling = false;

        // 롤링 종료 후 카메라 방향으로 회전
        if (Camera.main != null)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
            transform.rotation = targetRotation;

            // 모델 회전도 원래대로 유지하려면
            if (modelTransform != null)
                modelTransform.rotation = originalModelRotation;
        }
    }
}
