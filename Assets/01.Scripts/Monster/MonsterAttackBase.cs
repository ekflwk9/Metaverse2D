using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterAttackBase : MonoBehaviour
{
    protected MonsterBase monsterBase;
    protected MonsterProjectileController projectileController;
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

        Service.Log($"[MonsterAttackBase] monsterBase.AttackRange: {monsterBase.AttackRange}");
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

    public virtual bool CanPerformAttack()
    {
        player = GameManager.player.transform;
        direction = (player.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange) return false;

        // 공격이 끝났고 쿨타임도 지났을 때만 가능
        return isAttackEnd && (Time.time - lastAttackTime) >= attackSpeed;
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
