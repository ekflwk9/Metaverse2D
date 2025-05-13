using UnityEngine;

public class MeleeAttack : MonsterAttackBase
{
    protected override void DoAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
        if (hit != null)
        {
            GameManager.player.OnHit(attackDamage);
        }
    }
}
