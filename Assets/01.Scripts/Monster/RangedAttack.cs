using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonsterAttackBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        progectile.Shoot(transform.position, direction, attackSpeed, attackDamage);

        lastAttackTime = Time.time;
    }
}
