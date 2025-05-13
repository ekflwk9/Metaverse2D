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
        // �Ÿ��� �ָ� ������ false
        if (distance > attackRange)
            return false;

        // ���� ���̸� �Ÿ��� ������ true (��Ÿ�� ����)
        if (!isAttackEnd)
            return true;

        // ���� �������� �Ÿ� + ��Ÿ�� ��� �����ؾ� true
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
