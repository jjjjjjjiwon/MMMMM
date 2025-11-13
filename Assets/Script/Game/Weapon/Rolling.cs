using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    public float spinDuration = 1.0f;
    public float spinSpeed = 720f; // 초당 회전도
    public float spinRadius = 3f;
    public float damage = 30f;
    public LayerMask enemyLayer;

    private bool isSpinning = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isSpinning)
            StartCoroutine(SpinAttackRoutine());
    }

    IEnumerator SpinAttackRoutine()
    {
        isSpinning = true;
        float timer = 0f;

        while (timer < spinDuration)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

            Collider[] hits = Physics.OverlapSphere(transform.position, spinRadius, enemyLayer);
            foreach (var hit in hits)
            {
                Hitbox hb = hit.GetComponent<Hitbox>();
                if (hb != null)
                    hb.OnHit(damage);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        isSpinning = false;
    }
    
}
