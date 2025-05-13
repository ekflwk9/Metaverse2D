using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterMoveBase : MonoBehaviour
{
    protected Transform player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected MonsterBase monsterBase;
    protected MonsterAttackBase attackBase;
    protected SpriteRenderer spriteRenderer;
    
    
    protected float moveSpeed;

    protected bool isMoving;
    public bool IsMoving => isMoving;
    protected bool canMove;
    public bool CanMove => canMove;

    protected Vector2 direction;
    protected float distance;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        monsterBase = GetComponent<MonsterBase>();
        attackBase = GetComponent<MonsterAttackBase>();
    }

    protected virtual void Start()
    {
        player = GameManager.player.transform;
        moveSpeed = monsterBase.MoveSpeed;
    }

    private void Update()
    {
        canMove = CanPerformMove();
    }

    bool CanPerformMove()
    {
        return !attackBase.canAttack;
    }

    public virtual void OnMove()
    {
        if (monsterBase.IsDead) return;

        if (canMove)
        {
            if (anim != null)
            {
                anim.SetBool("isMoving", true);
            }

            Move();
        }
        else
        {
            StopMove();
        }
    }
    public abstract void Move();

    public virtual void StopMove()
    {
        if (anim != null)
        {
            anim.SetBool("isMoving", false);
        }

        //isMoving = false;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
