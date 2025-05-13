using UnityEngine;

public class RangedMovement : MonsterMoveBase
{
    private float keepDistance = 5f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > keepDistance)
        {
            rb.velocity = direction * moveSpeed;
            //isMoving = true;
        }
        else if (distance <= keepDistance)
        {
            base.StopMove();
        }
    }
}
