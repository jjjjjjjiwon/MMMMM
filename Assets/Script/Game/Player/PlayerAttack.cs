using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    // public WeaponHitbox weaponHitbox;
    // public float attackRange = 2f;     // 공격 사거리
    // public float attackDamage = 10f;   // 공격 데미지
    // public float attackRate = 1f;      // 초당 공격 횟수
    // private float nextAttackTime = 0f; // 공격 딜레이

    // void Update()
    // {
    //     if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
    //     {
    //         nextAttackTime = Time.time + 1f / attackRate;
    //         Attack();
    //     }
    // }

    // void Attack()
    // {
    //     Debug.Log("공격 실행!");
    //     weaponHitbox.EnableHitbox();

    //      // 0.3초 후에 콜라이더 끄기 (공격 애니메이션 타이밍 맞춰 조절 가능)
    //     Invoke(nameof(DisableWeapon), 0.3f);
    // }
    
    // void DisableWeapon()
    // {
    //     weaponHitbox.DisableHitbox();
    //     Debug.Log("공격 종료");
    // }

}
