using UnityEngine;

public class BossController : MonoBehaviour
{
    public int damage = 1;
    public int MonsterHp = 100;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MonsterAttack(damage);
        }
    }

    public void MonsterAttack(int damage)
    {
        MonsterHp -= damage;
        Debug.Log("Boss health: " + MonsterHp);

        if (MonsterHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Á×À½");
        Destroy(gameObject);
    }
}