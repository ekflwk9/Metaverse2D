using UnityEngine;

public class MeleeAttack : MonsterAttackBase
{
    protected override void DoAttack()
    {
         GameManager.player.OnHit(attackDamage);
    }

    public void TriggerAttack()
    {
        DoAttack();
    }
}
