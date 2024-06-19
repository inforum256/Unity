using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float turretRotationSpeed = 100f; // 포탑 회전 속도
    public GameObject projectilePrefab;
    public Transform firePoint; // 발사 지점

    private Rigidbody2D rb;
    private Transform turret;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        turret = transform.Find("Gun_01");
    }

    void Update()
    {
        // 탱크 이동 입력 (WASD)
        float moveVertical = 0f;
        float moveHorizontal = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed;
        rb.velocity = movement;

        // 포탑 회전 입력 (좌우 화살표 키)
        float turretRotation = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turretRotation = turretRotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            turretRotation = -turretRotationSpeed * Time.deltaTime;
        }

        if (turret != null)
        {
            turret.Rotate(0, 0, turretRotation);
        }

        // 발사 입력 (Enter 키)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Fire("PlayerProjectile");
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
}
