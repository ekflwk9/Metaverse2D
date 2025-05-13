using UnityEngine;

public class RangedMovement : MonsterMoveBase
{
    public override void Move()
    {
        Service.Log("Move ȣ�� �Ϸ�");
        float distance = Vector2.Distance(player.position, transform.position);
        Vector2 dir = (player.position - transform.position).normalized;

        Service.Log($"[Move] ���� ��ġ: {transform.position}, �÷��̾� ��ġ: {player.position}, ����: {dir}");

        if (distance > keepDistance)
        {
            rb.velocity = dir * moveSpeed;
        }
        else if (distance < keepDistance - 0.1f)
        {
            rb.velocity = -dir * moveSpeed * 0.5f;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        Service.Log($"[Move] ���� rb.velocity: {rb.velocity}");
    }
}
