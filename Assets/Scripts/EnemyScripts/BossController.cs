using UnityEngine;

public class BossController : MonoBehaviour
{
    public int damage = 1;
    public float bossHp = 100;
    public float maxHp = 100;
    public float attackCooldown = 3.0f; 
    public float attackRadius = 1.0f;
    private float lastAttackTime = 0.0f; 
    private Transform player;
    public delegate void HpChanged(); // 체력 변경 시 호출될 델리게이트
    public event HpChanged OnHpChanged; // 체력 변경 이벤트

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRadius && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Boss attacks player!");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        bossHp -= damage;
        OnHpChanged?.Invoke();

        if (bossHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("����");
        Destroy(gameObject);
    }
}