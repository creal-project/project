using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public force knockback;
    public float enemyHP = 20;

    public void Awake()
    {
        player = GameObject.Find("Player");
        enemyHP += RoomManager.Instance.roomGenerateCount*10;
    }
    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP <= 0)
        {
            Die();
        }
    }
    private void Update()
    {
        // if (player.GetComponent<Player>().isHit)
        // {
        //     if(enemyHP != 20){
        //         knockback.ApplyKnockback(-(player.transform.position-this.transform.position));
        //     }
        // }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
