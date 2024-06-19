using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5;
    public GameObject explosionEffectE;
    public GameObject explosionEffectF;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
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
