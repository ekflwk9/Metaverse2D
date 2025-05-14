using UnityEngine;

public class MeleeAttack : MonsterAttackBase
{
    protected override void DoAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.player.transform.position);

        if (distanceToPlayer <= monsterBase.attackRange)
        {
            GameManager.player.OnHit(attackDamage);
        }
        else
        {
            Service.Log($"공격 실패 : 범위 밖");
        }
    }

    public void TriggerAttack()
    {
        DoAttack();
    }
}
