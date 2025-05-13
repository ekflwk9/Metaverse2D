using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonsterAttackBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        isAttackEnd = false;
        GameManager.gameEvent.Hit(player.name, attackDamage);

        lastAttackTime = Time.time;
    }
}
