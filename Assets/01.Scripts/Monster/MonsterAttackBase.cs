using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttackBase : MonoBehaviour
{
    protected MonsterBase monsterBase;
    protected MonsterProgectileController progectile;
    protected float attackSpeed;
    protected int attackDamage;

    protected Transform player;
    protected Animator anim;

    protected bool isAttack = false;
    public bool IsAttack => isAttack;
    protected float lastAttackTime;

    protected virtual void Awake()
    {
        monsterBase = GetComponent<MonsterBase>();
        player = GameManager.player.transform;
    }
    public void SetAttack(float attackSpeed, int attackDamage)
    {
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
    }

    public abstract void Attack();

    protected IEnumerator AttackRoutine()
    {
        isAttack = true;

        monsterBase.anim.SetTrigger("isAttack");

        Attack();

        yield return new WaitForSeconds(attackSpeed);

        
        lastAttackTime = Time.time;
    }

    protected void StopAttack()
    {
        isAttack = false;

        if (anim != null)
        {
            anim.SetBool("isAttack", false);
        }
    }
}
