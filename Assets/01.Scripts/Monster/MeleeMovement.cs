using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement : MonsterMoveBase
{
    public override void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
