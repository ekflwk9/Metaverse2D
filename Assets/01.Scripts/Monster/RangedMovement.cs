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
        float distance = direction.magnitude;

        if (distance > keepDistance + 0.5f)
        {
            rb.velocity = direction * moveSpeed * Time.deltaTime;
            isMove = true;
        }
        else
        {
            StopMove();
        }
    }
}
