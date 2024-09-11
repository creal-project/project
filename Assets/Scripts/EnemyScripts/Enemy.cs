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
    CircleCollider2D colliderCircle;
    public Color damageColor = Color.red;
    private Color originalColor;

    public void Awake()
    {
        player = GameObject.Find("Player");
        enemyHP += RoomManager.Instance.roomGenerateCount*10;
        isAlive = true;
    }
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator =  GetComponent<Animator>();
        colliderCircle = GetComponent<CircleCollider2D>();
        animator.SetBool("isAlive",true);
        follow.enabled = true;
        colliderCircle.enabled = true;
        
        originalColor = spriteRenderer.color;
    }
    public void TakeDamage(float damage)
    {
        enemyHP -= damage;
        spriteRenderer.color = damageColor;
        StartCoroutine(ResetColorAfterDelay(0.5f));
    }
    private IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.color = originalColor;
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
        this.colliderCircle.enabled = false;
        //Destroy(gameObject);
    }
}
