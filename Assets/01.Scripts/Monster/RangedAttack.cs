using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonsterAttackBase
{
    private float attackRange = 6f;
    

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackSpeed)
            return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            progectile.Shoot(transform.position, direction, attackSpeed, attackDamage);
        }
        else
        {
            StopAttack();
        }
    }
}
