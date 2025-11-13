using UnityEngine;

public class DiveAttack : MonoBehaviour
{
    public float diveForce = 80f;
    public float impactRadius = 5f;
    public float damage = 60f;
    public LayerMask enemyLayer;
    public GameObject impactEffect;
    private Rigidbody rb;
    private bool isDiving = false;

    void Awake() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isDiving)
        {
            rb.velocity = Vector3.down * diveForce;
            isDiving = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isDiving) return;

        // 충돌 시 폭발 데미지
        Collider[] hits = Physics.OverlapSphere(transform.position, impactRadius, enemyLayer);
        foreach (var hit in hits)
        {
            Hitbox hb = hit.GetComponent<Hitbox>();
            if (hb != null)
                hb.OnHit(damage);
        }

        if (impactEffect)
            Instantiate(impactEffect, transform.position, Quaternion.identity);

        Debug.Log("💥 낙하 찌르기 타격!");
        isDiving = false;
    }
}
