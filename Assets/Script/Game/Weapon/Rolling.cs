using System.Collections;
using UnityEngine;

// Rigidbody가 반드시 있어야 함을 명시
[RequireComponent(typeof(Rigidbody))]
public class PlayerRolling : MonoBehaviour
{
    [Header("Rolling Settings")]
    public float rollForce = 30f;       // 롤링 추진력 (얼마나 강하게 나아갈지)
    public float rollDuration = 1f;     // 롤링이 지속되는 시간
    public float rollSpinSpeed = 1080f; // 롤링할 때 회전 속도 (도 단위/초)

    private Rigidbody rb;               // 플레이어의 물리 제어용 Rigidbody
    private bool isRolling = false;     // 현재 롤링 중인지 여부
    private WeaponHitbox weaponHitbox;  // 무기 히트박스 (충돌 시 데미지를 주는 영역)
    public PlayerGrapple playerGrapple; // 그래플링 상태를 가져오기 위한 참조

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();

        // 같은 오브젝트 또는 자식 오브젝트에 있는 WeaponHitbox 자동 검색
        weaponHitbox = GetComponentInChildren<WeaponHitbox>();
        if (weaponHitbox == null)
        {
            Debug.LogWarning("⚠ WeaponHitbox가 할당되지 않았습니다. Inspector에서 지정해주세요!");
        }
    }

    void Update()
    {
        // 🔹 E키 입력 & 현재 롤링 중이 아님 & 그래플링 중일 때만 롤링 가능
        if (Input.GetKeyDown(KeyCode.E) && !isRolling && playerGrapple != null && playerGrapple.IsGrappling)
        {
            StartCoroutine(DoRoll());
        }
    }

    // 🔸 롤링 동작 코루틴
    IEnumerator DoRoll()
    {
        // 히트박스 활성화 → 롤링 중에만 공격 가능
        if (weaponHitbox != null)
        {
            weaponHitbox.EnableHitbox();
           weaponHitbox.damage += 30f; // 임시로 공격력 강화 (필요 없으면 삭제 가능)
        }

        isRolling = true; // 롤링 시작
        float timer = 0f;

        // 현재 이동 중이라면 이동 방향, 아니면 정면으로 구름
        Vector3 rollDir = rb.velocity.sqrMagnitude > 0.01f ? rb.velocity.normalized : transform.forward;

        // 롤링 지속 시간 동안 반복
        while (timer < rollDuration)
        {
            // 🔹 앞 방향으로 추진력 가함 (그래플링 방향 또는 이동 방향)
            rb.AddForce(rollDir * rollForce, ForceMode.Acceleration);

            // 🔹 제자리 회전 (시각적 효과용)
            transform.Rotate(Vector3.down * rollSpinSpeed * Time.deltaTime, Space.Self);

            timer += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 롤링 종료
        isRolling = false;

        // 히트박스 비활성화 (공격 종료)
        if (weaponHitbox != null)
        {
            weaponHitbox.DisableHitbox();
        }
    }
}
