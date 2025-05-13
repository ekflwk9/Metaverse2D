using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterAttackBase : MonoBehaviour
{
    protected MonsterBase monsterBase;
    protected MonsterProgectileController progectile;
    protected float attackSpeed;
    protected int attackDamage;
    protected float attackRange;

    protected Transform player;
    protected Animator anim;

    public bool canAttack;
    
    public bool isAttackEnd;
    

    protected float lastAttackTime;
    public float LastAttackTime => lastAttackTime;

    protected Vector2 direction;
    protected float distance;

    protected virtual void Awake()
    {
        monsterBase = GetComponent<MonsterBase>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        player = GameManager.player.transform;

        attackSpeed = monsterBase.AttackSpeed;
        attackDamage = monsterBase.AttackDamage;
        attackRange = monsterBase.AttackRange;

        lastAttackTime = -attackSpeed;
        isAttackEnd = true;
    }

    private void Update()
    {
        canAttack = CanPerformAttack();
    }

    bool CanPerformAttack()
    {
        direction = (player.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.position);
        // 거리가 멀면 무조건 false
        if (distance > attackRange)
            return false;

        // 공격 중이면 거리만 맞으면 true (쿨타임 무시)
        if (!isAttackEnd)
            return true;

        // 공격 끝났으면 거리 + 쿨타임 모두 만족해야 true
        return (Time.time - lastAttackTime) >= attackSpeed;
    }
    public abstract void Attack();

    public virtual void OnAttack()
    {
        if (monsterBase.IsDead) return;

        if (anim != null)
        {
            anim.SetTrigger("isAttacking");
        }
    }
}
