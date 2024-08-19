using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject weapon;

    int atk;
    int hp;
    int def;
    int m_s;
    float a_c;

    bool attacking;

    void Start()
    {
        hp = 100;
        atk = 10;
        def = 50;
        attacking = false;
    }


    void Update()
    {
        Move();
    }


    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 movevector = new Vector3(hor, ver).normalized;
        transform.position += movevector * Time.deltaTime * 10;
    }

    void attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            attacking = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision .collider.gameObject.CompareTag("enemy"))
        {
            hp -= 20;
        }
    }

    void interactions()
    {
        if ()
        {
        
        }
    }



    /*void MA()//원거리 공격
    {
        void attack()
        {
            if (Input.GetKey(KeyCode.X))//공격키 임시 지정
            {
                isattack = true;//공격 모션
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
