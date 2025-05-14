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

public class MonsterBase : MonoBehaviour, IHit
{
    [SerializeField] private Vector2 bloodPos;
    [SerializeField] private Vector2 floorPos;

    [Header("Monster Type")]
    public MonsterType monsterType;

    private float normalSpeed;
    public float moveSpeed { get; private set; }
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }
    public float attackSpeed { get; private set; }
    public float attackRange { get; private set; }
    public float keepDistance { get; private set; }
    public int attackDamage { get; private set; }

    public bool IsDamaged { get; private set; }
    public bool IsDead => currentHealth <= 0;

    private SpriteRenderer spriteRenderer;
    public Animator animator { get; private set; }
    private Coroutine slowCoroutine;

    /// <summary>
    /// 몬스터 Awake
    /// </summary>
    public virtual void SetMonster()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        GameManager.SetComponent(this);

        SetStatsByType();
        currentHealth = maxHealth;
        normalSpeed = moveSpeed;
    }

    private void SetStatsByType()
    {
        switch (monsterType)
        {
            case MonsterType.BringerOfDeath:
                moveSpeed = 2f;
                maxHealth = 4f;
                attackSpeed = 1.5f;
                attackDamage = 1;
                attackRange = 1f;
                break;

            case MonsterType.FireWorm:
                moveSpeed = 1.5f;
                maxHealth = 4f;
                attackSpeed = 3.5f;
                attackDamage = 1;
                attackRange = 7f;
                keepDistance = 3f;
                break;

            case MonsterType.Necromancer:
                moveSpeed = 1.2f;
                maxHealth = 6f;
                attackSpeed = 2f;
                attackDamage = 1;
                attackRange = 6f;
                keepDistance = 5f;
                break;

            case MonsterType.Shaman:
                moveSpeed = 2.2f;
                maxHealth = 6f;
                attackSpeed = 3f;
                attackDamage = 1;
                attackRange = 4f;
                keepDistance = 2.5f;

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

    public void Dead()
    {
        if (IsDead)
        {
            animator.SetBool("isDead", true);
            StartCoroutine(DeactivateAfterDelay(10f));
        }
    }
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
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

    public void ApplySlow(float slowAmount)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(CoroutineSlow(slowAmount));
    }

    private IEnumerator CoroutineSlow(float slowAmount)
    {
        moveSpeed = normalSpeed * (1f - slowAmount);
        yield return Service.wait;
        moveSpeed = normalSpeed;
    }

    public void OnHit(int _dmg)
    {
        if (IsDead) return;

        currentHealth -= _dmg;
        IsDamaged = true;

        GameManager.effect.Show(bloodPos, "Blood");
        GameManager.effect.FloorBlood(floorPos);
        animator.SetTrigger("isDamaged");

        if (IsDead)
        {
            Dead();
            return;
        }

        StartCoroutine(ClearDamagedFlag());
    }

    private IEnumerator ClearDamagedFlag()
    {
        yield return new WaitForSeconds(0.3f);
        IsDamaged = false;
    }
}
