using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 30f; // 최대 체력
    private float currentHealth; // 현재 체력

    void Awake() => currentHealth = maxHealth; 

    // 데미지를 주는 
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} 피격! 남은 체력: {currentHealth}");

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} 사망!");
        Destroy(gameObject);
    }
    
}
