using UnityEngine;

public class RangedMovement : MonsterMoveBase
{
    private float keepDistance = 5f;

    public override void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = direction.magnitude;

        if (distance > keepDistance + 0.5f)
        {
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }
}
