using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; 
    public float stoppingDistance = 0.5f; 
    public float chaseDistance = 10f; 

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (distanceToPlayer > stoppingDistance)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime;
            }
        }
    }
}

