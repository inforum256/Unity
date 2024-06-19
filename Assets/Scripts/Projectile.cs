using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 10f; // 발사체의 생명 시간
    public string projectileTag; // 발사체의 태그

    void Start()
    {
        // 발사체의 태그 설정
        gameObject.tag = projectileTag;

        // 일정 시간 후에 발사체를 파괴합니다
        Destroy(gameObject, lifeTime);
    }
}
