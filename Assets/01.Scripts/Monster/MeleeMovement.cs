using UnityEngine;

public class MeleeMovement : MonsterMoveBase
{
    public override void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}
