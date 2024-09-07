using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;           
    public float attackRange = 2.0f;
    public int attackDamage = 2;
    public float enemyHP = 10;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange)
        {
           // Attack();
        }
        if(enemyHP<=0){
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }
}
