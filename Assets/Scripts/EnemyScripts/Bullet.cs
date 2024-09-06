using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;   // 총알 속도
    public float lifeTime = 1.25f; // 총알이 존재하는 시간
    public int damage = 10;      // 총알이 플레이어에게 가하는 피해 (Hp 감소량)

    void Start()
    {
        // 일정 시간이 지나면 총알을 파괴
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 총알을 앞으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 충돌 감지
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Wall" 태그를 가지고 있다면, 총알을 파괴
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있다면
        else if (collision.gameObject.CompareTag("Player"))
        {
            // PlayerHealth 스크립트에서 체력 감소 처리
            Player hp = collision.gameObject.GetComponent<Player>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            // 총알 파괴
            Destroy(gameObject);
        }
    }
}
