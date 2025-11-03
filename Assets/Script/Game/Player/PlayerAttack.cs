using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public WeaponHitbox weaponHitbox;
    public float attackRange = 2f;     // 공격 사거리
    public float attackDamage = 10f;   // 공격 데미지
    public float attackRate = 1f;      // 초당 공격 횟수
    private float nextAttackTime = 0f; // 공격 딜레이

    // PlayerRolling 스크립트 참조
    public PlayerRolling playerRolling;

    void Start()
    {
        // 자동 할당
        if (playerRolling == null)
            playerRolling = GetComponent<PlayerRolling>();
    }

    void Update()
    {
        // 공격 입력
        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;

            // 롤링 중에도 공격 가능하게 체크
            if (!playerRolling) 
            {
                Attack(); // 일반 공격
            }
            else
            {
                // 롤링 중 공격
                Attack(); 
                Debug.Log("롤링 공격!");
            }
        }
    }

    void Attack()
    {
        Debug.Log("공격 실행!");
        weaponHitbox.EnableHitbox();

        // 0.3초 후에 콜라이더 끄기 (공격 애니메이션 타이밍 맞춤)
        Invoke(nameof(DisableWeapon), 0.3f);
    }

    void DisableWeapon()
    {
        weaponHitbox.DisableHitbox();
        Debug.Log("공격 종료");
    }
}
