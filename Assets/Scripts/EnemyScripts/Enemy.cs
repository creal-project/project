using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public Sprite deadSprite;
    public bool isAlive;
    public float enemyHP = 20;
    public PlayerFollow follow;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CircleCollider2D collider;

    public void Awake()
    {
        player = GameObject.Find("Player");
        enemyHP += RoomManager.Instance.roomGenerateCount*10;
        isAlive = true;
    }
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator =  GetComponent<Animator>();
        collider = GetComponent<CircleCollider2D>();
        animator.SetBool("isAlive",true);
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
        isAlive = false;
        Debug.Log("Enemy died!");
        animator.SetBool("isAlive",false);
        follow.enabled = false;
        collider.enabled = false;
        //Destroy(gameObject);
    }
}
