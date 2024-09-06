using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SIngleTon<Player>
{
    public GameObject weapon;
    public GameObject enemy;

    public int hp=100;
    public int Hp_max=100;
    public int atk = 10;
    public float m_s;
    public float a_c;
    public int index;
    public int moveSpeed = 10;
    Rigidbody2D rb;

    bool attacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
    }


    private void Update()
    {
        PlayerMove();
    }


    void PlayerMove()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        
        Vector2 moveVector = new Vector2(hor, ver).normalized;

        
        transform.position += (Vector3)moveVector * moveSpeed * Time.deltaTime;
        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");

        //rb.velocity = new Vector3(moveX, moveY, 0) * moveSpeed;
    }

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            attacking = true;
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("enemy") || attacking==true)
        {
            enemy.GetComponent<EnemyAttack>().enemyHP -= atk;
        }
        else
        {
            TakeDamage(enemy.GetComponent<EnemyAttack>().attackDamage);
        }
    }


    public void TakeDamage(int attackDamage)
    {
        hp -= attackDamage;
    }



    /*void interactions()
    {
        if ()
        {
        
        }
    }*/
}
