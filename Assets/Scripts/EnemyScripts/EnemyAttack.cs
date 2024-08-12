using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;           // 플레이어의 Transform
    public float attackRange = 2.0f;   // 공격 범위 (거리에 따라 설정)
    public int attackDamage = 2;       // 적의 공격력
    private int playerHP = 10;         // 플레이어 HP

    void Update()
    {
        // 적과 플레이어 사이의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 플레이어가 공격 범위 내에 있는지 확인
        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        // 공격 실행
        TakeDamage(attackDamage);
    }

    void TakeDamage(int damage)
    {
        playerHP -= damage;
        Debug.Log("플레이어의 남은 체력: " + playerHP);

        if (playerHP <= 0)
        {
           // 나중에 함수 넣고
        }
    }
}
