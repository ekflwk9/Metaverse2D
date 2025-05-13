using UnityEngine;

public class RangedMovement : MonsterMoveBase
{
    public override void Move()
    {
        Service.Log("Move 호출 완료");
        float distance = Vector2.Distance(player.position, transform.position);
        Vector2 dir = (player.position - transform.position).normalized;

        Service.Log($"[Move] 몬스터 위치: {transform.position}, 플레이어 위치: {player.position}, 방향: {dir}");

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
        Service.Log($"[Move] 최종 rb.velocity: {rb.velocity}");
    }
}
