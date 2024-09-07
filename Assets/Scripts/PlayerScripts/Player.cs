using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;
    public GameObject enemy;

    public int hp=100;
    public int Hp_max=100;
    public float atk = 10; //attack
    public float movement_speed; //movement speed
    public float a_c; //attack cooldown
    public int moveSpeed = 10;
    Rigidbody2D rb;

    public bool attacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }    private void Update()
    {
        PlayerMove();
        attack();
    }
    void PlayerMove()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector2 moveVector = new Vector2(hor, ver).normalized;
        transform.position += (Vector3)moveVector * moveSpeed *movement_speed* Time.deltaTime;
    }

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            attacking = true;
        }
        else{
            attacking = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("enemy") || attacking==true)
        {
            collision.gameObject.GetComponent<EnemyAttack>().enemyHP -= atk;
        }
        else
        {
            TakeDamage(enemy.GetComponent<EnemyAttack>().attackDamage);
            Destroy(collision.gameObject);
        }
    }


    public void TakeDamage(int attackDamage)
    {
        hp -= attackDamage;
    }

    public bool IsDead() // If Player Death --> Scene Change(P_scene Script)
    {
        return hp <= 0;
    }
}
