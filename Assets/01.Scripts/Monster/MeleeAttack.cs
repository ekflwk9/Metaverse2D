using UnityEngine;

public class MeleeAttack : MonsterAttackBase
{
    protected override void DoAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.player.transform.position);

        if (distanceToPlayer <= monsterBase.attackRange)
        {
            GameManager.player.OnHit(attackDamage);
            isAttacking = false;
        }
        else
        {
            Service.Log($"���� ���� : ���� ��");
        }
    }

    public void TriggerAttack()
    {
        DoAttack();
    }
}
