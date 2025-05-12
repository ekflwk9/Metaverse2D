using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackStop : MonoBehaviour
{
    private MeleeAttack meleeAttack;

    private void Awake()
    {
        meleeAttack = GetComponentInParent<MeleeAttack>();
    }

    public void StopAttack()
    {
        if (meleeAttack != null)
        {
            meleeAttack.StopAttack();
        }
        else
        {
            Debug.LogError("MeleeAttack not found in parent!");
        }
    }
}
