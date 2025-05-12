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
    
    
    protected float moveSpeed;

    protected bool isMoving;
    public bool IsMoving => isMoving;
    protected bool canMove;
    public bool CanMove => canMove;
    protected bool isAttacking;

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
        isAttacking = attackBase.IsAttacking;
    }

    private void Update()
    {
        canMove = CanPerformMove();
    }

    bool CanPerformMove()
    {
        return !attackBase.CanAttack;
    }

    public virtual void OnMove()
    {
        if (monsterBase.IsDead) return;
        Debug.Log($"canMove: {canMove}");
        if (canMove)
        {
            if (anim != null)
            {
                anim.SetBool("isMove", true);
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
            anim.SetBool("isMove", false);
        }

        isMoving = false;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
