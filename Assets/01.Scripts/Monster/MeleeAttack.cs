using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonsterAttackBase
{
    private float attackRange = 1.0f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackSpeed)
            return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            GameManager.gameEvent.Hit(player.name, attackDamage);
        }
        else 
        { 
            StopAttack(); 
        }
    }
}
