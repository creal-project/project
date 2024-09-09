using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP = 50;
    public float attackDamage = 10; 

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
