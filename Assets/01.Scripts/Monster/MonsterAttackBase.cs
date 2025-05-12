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
        player = GameManager.player.transform;
    }
    private void Start()
    {
        attackSpeed = monsterBase.AttackSpeed;
        attackDamage = monsterBase.AttackDamage;
        attackRange = monsterBase.AttackRange;
    }

    private void Update()
    {
        CanPerformAttack();
    }
   

    bool CanPerformAttack() 
    {
        bool canAttack = ((Time.time - lastAttackTime) >= attackSpeed && distance <= attackRange);
        return canAttack; 
    }
    public abstract void Attack();

    public virtual void OnAttack()
    {
        if (monsterBase.IsDead) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (canAttack)
        {
            if (anim != null)
            {
                anim.SetBool("isAttack", true);
            }

            Attack();
        }
        else
        {
            StopAttack();
        }
    }

    public virtual void StopAttack()
    {
        isAttacking = false;

        if (anim != null)
        {
            anim.SetBool("isAttack", false);
        }
    }
}
