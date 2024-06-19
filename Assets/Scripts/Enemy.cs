using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject explosionEffectE;
    public GameObject explosionEffectF;
    public float fireRate = 2f; // 발사 주기 (초)
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Fire("EnemyProjectile");
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire(string projectileTag)
    {
        if (firePoint != null && projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = firePoint.up * 10f; // 발사 속도 설정

            // 발사체의 태그 설정
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.projectileTag = projectileTag;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        ShowEffect(explosionEffectE, transform.position);
        if (health <= 0)
        {
            ShowEffect(explosionEffectF, transform.position);
            Destroy(gameObject);
        }
    }

    void ShowEffect(GameObject effect, Vector3 position)
    {
        if (effect != null)
        {
            GameObject instantiatedEffect = Instantiate(effect, position, Quaternion.identity);
            Destroy(instantiatedEffect, 0.5f); // 0.5초 후에 이펙트를 파괴
        }
    }
}
