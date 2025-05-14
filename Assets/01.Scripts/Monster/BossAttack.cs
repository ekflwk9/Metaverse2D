using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    public enum AttackType { Dash, Ranged }

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dashSpeedMultiplier = 3f;
    [SerializeField] private float dashCooldownTime = 2f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private RangedAttack rangedAttack;
    private MonsterAttackBase attackBase;

    private bool isAttacking = false;
    private bool dashOnCooldown = false;
    private Vector2 dashDirection;

    public bool IsAttacking => isAttacking;

    private void Start()
    {
        attackBase = GetComponent<RangedAttack>();
    }

    public void PerformAttack()
    {
        AttackType nextType = (AttackType)Random.Range(0, 2);

        if (nextType == AttackType.Dash && !dashOnCooldown)
        {
            StartCoroutine(DashAttack());
        }
        else if (nextType == AttackType.Ranged && attackBase != null)
        {
            isAttacking = true;
            anim.SetTrigger("rangedAttack");
            attackBase.StartAttack(() => EndAttack());
        }
    }

    private IEnumerator DashAttack()
    {
        isAttacking = true;
        dashOnCooldown = true;

        dashDirection = (GameManager.player.transform.position - transform.position).normalized;

        anim.SetTrigger("dashAttack");
        rb.velocity = dashDirection * dashSpeedMultiplier;

        StartCoroutine(DashCooldown());
        yield break; 
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        dashOnCooldown = false;
    }

    public void TriggerDashDamage()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            GameManager.player.OnHit(3);
        }
        else
        {
            Service.Log("DashAttack 범위 밖에 있습니다");
        }
    }

    public void TriggerProjectile()
    {
        rangedAttack?.SpawnProjectile();
    }

    public void EndAttack()
    {
        Service.Log("[BossAttack] 보스는 멈추도록 하시오");
        rb.velocity = Vector2.zero;
        isAttacking = false;

        FindObjectOfType<BossController>()?.ResetAttackCooldown();
    }
}
