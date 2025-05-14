using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnDelay : MonoBehaviour
{
    private BossAttack bossAttack;
    private RangedAttack rangedAttack;
    private Rigidbody2D rb;

    void Start()
    {
        bossAttack = GetComponentInParent<BossAttack>();
        rangedAttack = GetComponentInParent<RangedAttack>();
        rb = GetComponentInParent<Rigidbody2D>();
        
    }

    public void DelayProjectileSpawn()
    {
        if (rangedAttack != null)
        {
            rangedAttack.SpawnProjectile();
        }
    }
    public void EndAttack()
    {
        rb.velocity = Vector2.zero;
        bossAttack.isAttacking = false;
    }
}
