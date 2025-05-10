using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement : MonsterMoveBase
{
    private float keepDistance = 0.3f;
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
