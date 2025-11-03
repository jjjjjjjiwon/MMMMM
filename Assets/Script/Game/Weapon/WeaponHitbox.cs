using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public float damage = 10f; // 무기 데미지
    private bool canDamage = false; // 데미지 활성화 여부
    public bool CanDamage => canDamage;


    void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; // 활성화되지 않았으면 아무것도 안 함

        if (other.CompareTag("Enemy")) // Enemy 태그 감지
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // 데미지 적용
                Debug.Log($"Hit {other.name} for {damage} damage");
            }
        }
    }

    // 히트박스 활성화
    public void EnableHitbox()
    {
        canDamage = true;
        gameObject.SetActive(true);
    }

    // 히트박스 비활성화
    public void DisableHitbox()
    {
        canDamage = false;
        gameObject.SetActive(false);
    }
}
