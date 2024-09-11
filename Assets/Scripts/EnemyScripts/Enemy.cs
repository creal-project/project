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
        follow.enabled = true;
        collider.enabled = true;
    }
    public void TakeDamage(float damage)
    {
        enemyHP -= damage;
    }
    private void Update()
    {
        // if (player.GetComponent<Player>().isHit)
        // {
        //     if(enemyHP != 20){
        //         knockback.ApplyKnockback(-(player.transform.position-this.transform.position));
        //     }
        // }
        if (enemyHP <= 0)
        {
            Die();
            follow.StopAllCoroutines();
            follow.enabled = false;
        }
    }

    private void Die()
    {
        this.isAlive = false;
        //Debug.Log("Enemy died!");
        this.animator.SetBool("isAlive",false);
        this.collider.enabled = false;
        //Destroy(gameObject);
    }
}
