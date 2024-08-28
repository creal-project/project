using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;           // �÷��̾��� Transform
    public float attackRange = 2.0f;   // ���� ���� (�Ÿ��� ���� ����)
    public int attackDamage = 2;       // ���� ���ݷ�
    private int playerHP = 10;         // �÷��̾� HP

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        // ���� ����
        TakeDamage(attackDamage);
    }

    void TakeDamage(int damage)
    {
        playerHP -= damage;
        Debug.Log("�÷��̾��� ���� ü��: " + playerHP);

        if (playerHP <= 0)
        {
           // ���߿� �Լ� �ְ�
        }
    }
}
