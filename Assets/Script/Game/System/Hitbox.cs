using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Health targetHealth; // 이 히트박스가 소속된 캐릭터의 체력

    void Awake()
    {
        if (targetHealth == null)
            targetHealth = GetComponentInParent<Health>(); // 부모에서 찾기
    }

    public void OnHit(float damage)
    {
        targetHealth.TakeDamage(damage);
    }
}
