using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{

    public enum MonsterType
    {
        BingerOfDeath,
        FireWorm,
        Necromancer,
        Shaman,
        Boss
    }

    [Header("Monster Type")]
    public MonsterType monsterType;

    [SerializeField] private SpriteRenderer characterRenderer;

    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }

    private bool isDead = false;
    public bool IsDead { get { return isDead; } }

    public float currentHealth;

    private Collider2D _collider;
    public Animator anim;

    public bool CanMove = true;

    private MonsterAttackBase attackBase;


    void Awake()
    {
        InitializeStats(monsterType);

        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        attackBase = GetComponent<MonsterAttackBase>();

        InitializeAttack();
    }


    void InitializeStats(MonsterType type)
    {
        switch(type)
        {
            case MonsterType.BingerOfDeath:
                moveSpeed = 1.4f;
                maxHealth = 4f;
                attackDamage = 1;
                break;

            case MonsterType.FireWorm:
                moveSpeed = 1f;
                maxHealth = 4f;
                attackDamage = 1;
                attackSpeed = 1f;
                break;

            case MonsterType.Necromancer:
                moveSpeed = 1f;
                maxHealth = 6f;
                attackDamage = 1;
                attackSpeed = 1.5f;
                break;

            case MonsterType.Shaman:
                moveSpeed = 1.6f;
                maxHealth = 6f;
                attackDamage = 1;
                attackSpeed = 2f;
                break;

            case MonsterType.Boss:
                moveSpeed = 2f;
                maxHealth = 12f;
                attackDamage = 1;
                attackSpeed = 2.5f;
                break;

        }
    }

    void InitializeAttack()
    {
        if (attackBase != null)
        {
            attackBase.SetAttack(attackSpeed, attackDamage);
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        anim.SetTrigger("isDamaged");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        anim.SetTrigger("Die");

        CanMove = false;
        _collider.enabled = false;

        StartCoroutine(DisableAfterDeath());
    }

    private IEnumerator DisableAfterDeath()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
