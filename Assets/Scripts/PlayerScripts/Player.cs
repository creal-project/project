using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;

    int hp;
    int hp_max;
    int atk;
    int m_s;
    float a_c;
    int index;
    public int moveSpeed = 10;
    Rigidbody2D rb;

    bool attacking;

    void Start()
    {
<<<<<<< Updated upstream
        rb = GetComponent<Rigidbody2D>();
=======

        attacking = false;
        hp_max = 100;
>>>>>>> Stashed changes
        hp = 100;
        atk = 10;
        m_s = 1;
        a_c = 1;
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
        if (collision.collider.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }



    /*void interactions()
    {
        if ()
        {
        
        }
    }*/



    /*void MA()//���Ÿ� ����
    {
        void attack()
        {
            if (Input.GetKey(KeyCode.X))//����Ű �ӽ� ����
            {
                isattack = true;//���� ���
                if (isattack == true)
                {
                    animator.SetBool("attack", true);
                    GameObject newbullet = Instantiate(bullet, shootpoint.position, transform.rotation * Quaternion.identity);
                }
            }
            else
            {
                isattack = false;
                animator.SetBool("attack", false);
            }
        }
    }*/
}
