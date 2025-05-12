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
        if (distance > keepDistance)
        {
            rb.velocity = direction * moveSpeed * Time.deltaTime;
            isMoving = true;
        }
        else if (distance <= keepDistance)
        {
            base.StopMove();
        }
    }
}
