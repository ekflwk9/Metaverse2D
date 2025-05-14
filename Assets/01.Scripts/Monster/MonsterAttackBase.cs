using UnityEngine;
using System.Collections;
using System;

public abstract class MonsterAttackBase : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;

    public float attackCooldown { get; private set; }

    protected int attackDamage;
    protected bool isAttacking = false;
    protected Action onAttackComplete;

    protected MonsterBase monsterBase;

    public bool IsAttackEnd => !isAttacking;

    protected virtual void Start()
    {
        monsterBase = GetComponent<MonsterBase>();
        attackCooldown = monsterBase.attackSpeed;
        attackDamage = monsterBase.attackDamage;
    }

    public void StartAttack(Action onComplete)
    {
        Service.Log($"StartAttack 호출됨? isAttacking: {isAttacking}");
        if (!isAttacking)
            StartCoroutine(AttackRoutine(onComplete));
    }

    private IEnumerator AttackRoutine(Action onComplete)
    {
        isAttacking = true; // 쿨타임 시작
        Service.Log($"[AttackRoutine] 쿨타임: {attackCooldown}");

        yield return new WaitForSeconds(attackCooldown); // 쿨타임 대기
        isAttacking = false;

        onComplete?.Invoke(); // 공격 종료 알림
    }

    protected abstract void DoAttack();

    public bool CanPerformAttack() => !isAttacking;
}
