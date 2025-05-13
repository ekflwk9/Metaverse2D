using System;
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

    [SerializeField] private Transform spritePivot;

    protected private MonsterAttackBase attackBase;
    protected private MonsterMoveBase moveBase;

    private float moveSpeed;
    private float normalSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    private int attackDamage;
    public int AttackDamage { get { return attackDamage; } }
    private float attackRange;
    public float AttackRange { get { return attackRange; } }

    private bool isDead = false;
    public bool IsDead { get { return isDead; } }
    private bool isDamaged = false;
    public bool IsDamaged {  get { return isDamaged; } }

    private float currentHealth;
    
    private Coroutine slowCoroutine;

    private Collider2D _collider;
    private Animator anim;

    void Awake()
    {
        InitializeStats(monsterType);

        currentHealth = maxHealth;
        normalSpeed = moveSpeed;

        anim = GetComponentInChildren<Animator>();
        _collider = GetComponentInChildren<Collider2D>();
        spritePivot = transform.Find("SpritePivot");
    }

    void InitializeStats(MonsterType type)
    {
        Service.Log($"[MonsterBase] InitializeStats called for {type}");
        switch (type)
        {
            case MonsterType.BingerOfDeath:
                moveSpeed = 1.4f;
                currentHealth = 4f;
                maxHealth = 4f;
                attackSpeed = 2f;
                attackDamage = 1;
                attackRange = 1.5f;
                break;

            case MonsterType.FireWorm:
                moveSpeed = 1f;
                currentHealth = 4f;
                maxHealth = 4f;
                attackDamage = 1;
                attackSpeed = 6f;
                attackRange = 6f;
                break;

            case MonsterType.Necromancer:
                moveSpeed = 1f;
                currentHealth = 6f;
                maxHealth = 6f;
                attackDamage = 1;
                attackSpeed = 1.5f;
                attackRange = 6f;
                break;

            case MonsterType.Shaman:
                moveSpeed = 1.6f;
                currentHealth = 6f;
                maxHealth = 6f;
                attackDamage = 1;
                attackSpeed = 2f;
                attackRange = 6f;
                break;

            case MonsterType.Boss:
                moveSpeed = 2f;
                currentHealth = 12f;
                maxHealth = 12f;
                attackDamage = 1;
                attackSpeed = 2.5f;
                attackRange = 6f;
                break;

        }
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        //플레이어 스킬과 충돌
        isDamaged = true;

        currentHealth -= amount;
        anim.Play("Damage");

        isDamaged = false;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        if (isDead) return;

        isDead = true;
        anim.Play("Dead");

        _collider.enabled = false;

        StartCoroutine(DisableAfterDeath());
    }

    private IEnumerator DisableAfterDeath()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public virtual void SetIdle()
    {
        if (isDead) return;

        if(anim != null)
        {
            anim.SetBool("isMoving", false);
        }
    }

    public virtual void FlipMainSprite()
    {
        //if (attackBase.canAttack) 
        //    return;

        Vector3 scale = spritePivot.localScale;

        if (GameManager.player.transform.position.x < transform.position.x)
        {
            scale.x = 1;
        }
        else
        {
            scale.x = -1;
        }

        spritePivot.localScale = scale;
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
}