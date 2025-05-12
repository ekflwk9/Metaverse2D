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

    protected bool isAttacking;
    public bool IsAttacking => isAttacking;
    protected bool canAttack;
    public bool CanAttack => canAttack;

    protected float lastAttackTime;
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

        isAttacking = false;
    }

    private void Update()
    {
        canAttack = CanPerformAttack();
    }

    bool CanPerformAttack() 
    {
        direction = (player.position - transform.position).normalized;
        distance = Vector2.Distance(transform.position, player.position);

        return (!isAttacking && (Time.time - lastAttackTime) >= attackSpeed && distance <= attackRange);
    }
    public abstract void Attack();

    public virtual void OnAttack()
    {
        if (monsterBase.IsDead) return;

        if (canAttack)
        {

            if (anim != null)
            {
                anim.SetTrigger("isAttack");
            }
            Attack();
        }
        else
        {
            StopAttack();
        }

    }

    public abstract void StopAttack();
    
}
