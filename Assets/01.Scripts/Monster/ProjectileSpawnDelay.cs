using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnDelay : MonoBehaviour
{
    private RangedAttack rangedAttack;

    void Start()
    {
        rangedAttack = GetComponentInParent<RangedAttack>();
    }

    public void RelayProjectileSpawn()
    {
        if (rangedAttack != null)
        {
            rangedAttack.SpawnProjectile();
        }
    }

}
