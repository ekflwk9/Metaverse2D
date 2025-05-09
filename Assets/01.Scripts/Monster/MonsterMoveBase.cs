using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterMoveBase : MonoBehaviour
{
    protected Transform player;
    protected Rigidbody2D rb;
    protected Animator anim;
    private MonsterBase monsterBase;
    private MonsterAttackBase attackBase;
    
    
    protected float moveSpeed;

    protected bool isMove;
    public bool IsMove => isMove;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
        
    protected virtual void Start()
    {
        player = GameManager.player.transform;
        monsterBase = GetComponent<MonsterBase>();
        moveSpeed = monsterBase.MoveSpeed;
    }

    public virtual void OnMove()
    {
        if (monsterBase.IsDead) return;

        isMove = true;
        if (anim != null)
        {
            anim.SetBool("isMove", true);
        }

        Move();
    }
    public abstract void Move();

    protected void StopMove()
    {
        isMove = false;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        if (anim != null)
        {
            anim.SetBool("isMove", false);
        }
    }
}
