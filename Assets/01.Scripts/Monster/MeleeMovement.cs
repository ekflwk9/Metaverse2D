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
        float distance = direction.magnitude;

        if (distance > keepDistance)
        {
            rb.velocity = direction * moveSpeed * Time.deltaTime;
            isMove = true;
        }
        if (distance <= keepDistance)
        {
            StopMove();
        }
    }
}
