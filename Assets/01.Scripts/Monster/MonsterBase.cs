using UnityEngine;
using System.Collections;

public enum MonsterType
{
    BringerOfDeath,
    FireWorm,
    Necromancer,
    Shaman,
    Boss
}

public class MonsterBase : MonoBehaviour
{
    [Header("Monster Type")]
    public MonsterType monsterType;
    
    public float moveSpeed { get; private set; }
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }
    public float attackSpeed { get; private set; }
    public int attackDamage { get; private set; }
    public float attackRange { get; private set; }
    public float keepDistance { get; private set; }
    
    public bool IsDamaged { get; private set; }
    public bool IsDead => currentHealth <= 0;

    private SpriteRenderer spriteRenderer;
    public Animator animator { get; private set; }

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SetStatsByType();
        currentHealth = maxHealth;
    }

    private void SetStatsByType()
    {
        switch (monsterType)
        {
            case MonsterType.BringerOfDeath:
                moveSpeed = 1.6f;
                maxHealth = 4f;
                attackSpeed = 2f;
                attackDamage = 1;
                attackRange = 1.5f;
                break;

            case MonsterType.FireWorm:
                moveSpeed = 1.5f;
                maxHealth = 4f;
                attackSpeed = 3.5f;
                attackDamage = 1;
                attackRange = 7f;
                keepDistance = 5f;
                break;

            case MonsterType.Necromancer:
                moveSpeed = 1.2f;
                maxHealth = 6f;
                attackSpeed = 2f;
                attackDamage = 1;
                attackRange = 6f;
                keepDistance = 7f;
                break;

            case MonsterType.Shaman:
                moveSpeed = 2.2f;
                maxHealth = 6f;
                attackSpeed = 3f;
                attackDamage = 1;
                attackRange = 5f;
                keepDistance = 3f;

                break;

            case MonsterType.Boss:
                moveSpeed = 2f;
                maxHealth = 12f;
                attackSpeed = 2f;
                attackDamage = 1;
                attackRange = 4.5f;
                keepDistance = 2f;

                break;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (IsDead) return;
        currentHealth -= damage;
        IsDamaged = true;
        animator.SetTrigger("isDamaged");
        StartCoroutine(ClearDamagedFlag());
    }

    private IEnumerator ClearDamagedFlag()
    {
        yield return new WaitForSeconds(0.5f);
        IsDamaged = false;
    }

    public void Dead()
    {
        if (IsDead)
        {
            animator.SetBool("isDead", true);
            gameObject.SetActive(false); // or Destroy(gameObject);
        }
    }

    public void SetIdle()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
    }

    public void FlipMainSprite()
    {
        if (spriteRenderer == null) return;
        Vector3 scale = transform.localScale;
        scale.x = (GameManager.player.transform.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = scale;
    }
}
