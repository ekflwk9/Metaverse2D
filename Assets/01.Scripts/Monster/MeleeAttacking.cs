using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacking : MonoBehaviour
{
    private MeleeAttack meleeAttack;
    private MonsterAttackBase attackBase;
    protected bool isAttackEnd;
    protected bool canAttack;
    

    private void Awake()
    {
        meleeAttack = GetComponentInParent<MeleeAttack>();
        attackBase = GetComponentInParent<MonsterAttackBase>();
        isAttackEnd = attackBase.isAttackEnd;
        canAttack = attackBase.canAttack;

    }

    public void Attack()
    {
        if (meleeAttack != null)
        {
            meleeAttack.Attack();
        }
    }

    public void AttackEnd()
    {
        isAttackEnd = true;
    }
}
