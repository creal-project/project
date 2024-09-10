using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public int Hp_max = 100;
    public float atk = 10; // 공격력
    public float movementSpeed = 10;
    public float attackRadius = 2.0f;
    public float attackCooldown = 2f;   
    public LayerMask enemyLayer; // 적 레이어
    Animator animator;
    SpriteRenderer spriteRenderer;
    public delegate void HpChanged(); 
    public delegate void CdChanged(); 
    public event HpChanged OnHpChanged; // 체력 변경 이벤트
    public event CdChanged OnCdChanged; // 쿨타임 변경 이벤트

    private Rigidbody2D rb;
    private bool canAttack = true;
    public float currentAttackCooldown = 0f; // 현재 쿨다운 변수

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMove();
        HandleAttack();
    }

    void PlayerMove()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if(ver != 0 || hor !=  0)
        {
            if(hor<0)
            {
                spriteRenderer.flipX = true;
            }
            else{
                spriteRenderer.flipX = false;
            }
            animator.SetBool("MoveBool",true);
            
        }
        else{
            animator.SetBool("MoveBool",false);
        }
        Vector2 moveVector = new Vector2(hor, ver).normalized;
        transform.position += (Vector3)moveVector * movementSpeed * Time.deltaTime;
        
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            Debug.Log("@@");
            Attack();
        }

        if (!canAttack)
        {
            currentAttackCooldown += Time.deltaTime;
            if (currentAttackCooldown >= attackCooldown)
            {
                canAttack = true;
                currentAttackCooldown = 0f;
                OnCdChanged?.Invoke();
            }
        }
    }

    void Attack()
    {
        canAttack = false;
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayer);
        Debug.Log("123");
        foreach (Collider2D target in targets)
        {
            
            if (target != null)
            {
                Enemy enemyAttack = target.GetComponent<Enemy>();
                BossController bossController = target.GetComponent<BossController>();
                
                if (enemyAttack != null)
                {
                    enemyAttack.TakeDamage(atk);
                    Debug.Log($"{atk} damage to Enemy");
                }
                else if (bossController != null)
                {
                    bossController.TakeDamage(atk);
                    Debug.Log($"{atk} damage to Boss");
                }
            }
        }
    }

    public void TakeDamage(float attackDamage)
    {
        hp -= (int)attackDamage;
        OnHpChanged?.Invoke(); // 체력 변경 이벤트 호출
    }

    public bool IsDead() // 플레이어 사망 체크
    {
        return hp <= 0;
    }
}
