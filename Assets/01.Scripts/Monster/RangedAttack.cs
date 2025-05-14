using UnityEngine;

public class RangedAttack : MonsterAttackBase
{
    [SerializeField] private float projectileSpeed = 3f;

    protected override void DoAttack()
    {
        if (projectilePrefab == null)
        {
            Service.Log($"{gameObject.name} 는 Projectile 프리팹을 갖고 있지 않습니다.");
            return;
        }

        Vector2 dir = (GameManager.player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * projectileSpeed;
        }

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDamage(attackDamage);
        }
    }

    public void SpawnProjectile()
    {
        DoAttack();
    }
}
