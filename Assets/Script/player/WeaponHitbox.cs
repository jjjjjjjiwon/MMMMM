using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public float damage = 10f; // 무기 데미지
    private bool canDamage = false; // 데미지 활성화

    void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; // 데미지 활성화가 안 되면 공격 안 되게

        if (other.CompareTag("Enemy")) // 태그가 Enemy이면
        {
                Debug.Log("테그");

            EnemyHealth enemy = other.GetComponent<EnemyHealth>(); // EnemyHealth를 가지고 있으면
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Enemy에게 데미지를 준다
                Debug.Log("데미지");
                canDamage = false; // 한 번에 한 대상만 데미지 줄 수도 있음
            }
        }
    }

    // hit박스 활성화
    public void EnableHitbox()
    {
        canDamage = true;
        gameObject.SetActive(true);
    }

    // hit박스 비활성화
    public void DisableHitbox()
    {
        canDamage = false;
        gameObject.SetActive(false);
    }
}
