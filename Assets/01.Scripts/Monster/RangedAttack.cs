using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonsterAttackBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        projectileController = GetComponent<MonsterProjectileController>();
    }

    public override void Attack()
    {
        isAttackEnd = false;
        lastAttackTime = Time.time;

        projectileController.Shoot(transform.position, direction, 3f, attackDamage);
    }
}
