using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttacking : MonoBehaviour
{
    private RangedAttack rangedAttack;
    private MonsterAttackBase attackBase;
    protected bool canAttack;


    private void Awake()
    {
        rangedAttack = GetComponentInParent<RangedAttack>();
        attackBase = GetComponentInParent<MonsterAttackBase>();
        canAttack = attackBase.canAttack;

    }

    public void Attack()
    {
        if (rangedAttack != null)
        {
            rangedAttack.Attack();
        }
    }

    public void AttackEnd()
    {
        attackBase.isAttackEnd = true;
    }
}
