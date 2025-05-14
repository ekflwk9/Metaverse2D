using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    public enum AttackType { Dash, Ranged }

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float dashSpeedMultiplier = 3f;
    [SerializeField] private float dashCooldownTime = 2f;
    [SerializeField] private MonsterAttackBase attackBase;

    private bool isAttacking = false;
    private bool dashOnCooldown = false;
    private Vector2 dashDirection;

    public bool IsAttacking => isAttacking;

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
        yield break; // EndAttack�� �ִϸ��̼� �̺�Ʈ���� ȣ���
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        dashOnCooldown = false;
    }

    public void TriggerDashDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 1.5f, LayerMask.GetMask("Player"));
        if (hit != null)
        {
            GameManager.player.OnHit(20); // ���� ������
        }
    }

    public void TriggerProjectile()
    {
        // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǵ� Projectile ó�� (�̹� attackBase���� �߻��)
    }

    public void EndAttack()
    {
        rb.velocity = Vector2.zero;
        isAttacking = false;

        FindObjectOfType<BossController>()?.ResetAttackCooldown();
    }
}
