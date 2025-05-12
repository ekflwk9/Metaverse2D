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
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(player.position, transform.position);

        if (distance > keepDistance)
        {
            rb.velocity = direction * moveSpeed;
            isMoving = true;
        }
        else
        {
            StopMove();
        }
    }
}
