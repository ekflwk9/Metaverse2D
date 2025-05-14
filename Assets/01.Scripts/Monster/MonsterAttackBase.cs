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
        Service.Log($"StartAttack ȣ���? isAttacking: {isAttacking}");
        if (!isAttacking)
            StartCoroutine(AttackRoutine(onComplete));
    }

    private IEnumerator AttackRoutine(Action onComplete)
    {
        isAttacking = true; // ��Ÿ�� ����
        Service.Log($"[AttackRoutine] ��Ÿ��: {attackCooldown}");

        yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���
        //isAttacking = false;

        onComplete?.Invoke();
    }

    protected abstract void DoAttack();

    public bool CanPerformAttack() => !isAttacking;
}
