using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float wanderSpeed = 1.5f;
    [SerializeField] private float attackCooldownTime = 3f;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private MonsterBase monsterBase;

    private float attackCooldown;
    private bool hasWanderDirection = false;
    private Vector2 wanderDirection;

    private void Start()
    {
        attackCooldown = attackCooldownTime;
        PickNewWanderDirection();
    }

    private void Update()
    {
        monsterBase.FlipMainSprite();

        if (!bossAttack.IsAttacking)
        {
            anim.SetBool("isMoving", true);
            Wander();
        }
        else
        {
            anim.SetBool("isMoving", false);
            hasWanderDirection = false;
        }

        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f && !bossAttack.IsAttacking)
        {
            bossAttack.PerformAttack();
        }
    }

    private void Wander()
    {
        if (!hasWanderDirection)
        {
            PickNewWanderDirection();
            hasWanderDirection = true;
        }

        rb.velocity = wanderDirection * wanderSpeed * 0.8f;
    }

    private void PickNewWanderDirection()
    {
        wanderDirection = Random.insideUnitCircle.normalized;
    }

    public void ResetAttackCooldown()
    {
        attackCooldown = attackCooldownTime;
    }
}
