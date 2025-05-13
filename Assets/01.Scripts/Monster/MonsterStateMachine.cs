using Unity.VisualScripting;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Attack,
    Damaged,
    Dead
}

public class MonsterStateMachine : MonoBehaviour
{
    private MonsterState currentState;
    private MonsterState previousState;
    private MonsterMoveBase moveBase;
    private MonsterAttackBase attackBase;
    private MonsterBase monsterBase;
    //protected MeleeAttacking meleeAttacking;

    bool isAttackEnd;

    void Awake()
    {
        moveBase = GetComponent<MonsterMoveBase>();
        attackBase = GetComponent<MonsterAttackBase>();
        monsterBase = GetComponent<MonsterBase>();
        //meleeAttacking = GetComponentInChildren<MeleeAttacking>();
    }

    void Start()
    {
        currentState = MonsterState.Idle;
        //isAttackEnd = meleeAttacking.isAttackEnd;
    }

    void Update()
    {
        Service.Log($"[StateMachine] Current State: {currentState}");

        switch (currentState)
        {
            case MonsterState.Idle:
                IdleUpdate();
                break;

            case MonsterState.Move:
                MoveUpdate();
                break;

            case MonsterState.Attack:
                AttackUpdate();
                break;

            case MonsterState.Damaged:
                DamagedUpdate();
                break;

            case MonsterState.Dead:
                DeadUpdate();
                break;
        }
    }

    void ChangeState(MonsterState newState)
    {
        if (currentState == newState)
            return;

        previousState = currentState;

        if (previousState == MonsterState.Move)
        {
            moveBase.StopMove();
        }

        currentState = newState;
        OnStateEnter(newState);
    }

    void OnStateEnter(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                monsterBase.SetIdle();
                break;

            case MonsterState.Attack:
                attackBase.OnAttack();
                break;

            case MonsterState.Move:
                break;

            case MonsterState.Damaged:
                monsterBase.TakeDamage(GameManager.player.dmg);
                break;

            case MonsterState.Dead:
                monsterBase.Dead();
                break;

        }
    }

    void IdleUpdate()
    {
        monsterBase.FlipMainSprite();

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        if (attackBase.canAttack && attackBase.isAttackEnd)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (moveBase.CanMove)
        {
            ChangeState(MonsterState.Move);
        }
        else if (monsterBase.IsDamaged)
        {
            ChangeState(MonsterState.Damaged);
        }
    }
    void MoveUpdate()
    {
        moveBase.OnMove();
        monsterBase.FlipMainSprite();

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        if (attackBase.canAttack && attackBase.isAttackEnd)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (monsterBase.IsDamaged)
        {
            ChangeState(MonsterState.Damaged);
        }
    }

    void AttackUpdate()
    {
        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
            return;
        }

        if (monsterBase.IsDamaged)
        {
            ChangeState(MonsterState.Damaged);
            return;
        }

        // 공격이 끝났으면...
        if (attackBase.isAttackEnd)
        {
            if (attackBase.CanPerformAttack())
            {
                // 공격 가능한 상태니까 반복 공격
                attackBase.OnAttack();
                attackBase.Attack();
            }
            else
            {
                // 공격도 못하고 끝났으면 → 다른 상태로 전이
                if (moveBase.CanMove)
                    ChangeState(MonsterState.Move);
                else
                    ChangeState(MonsterState.Idle);
            }
        }
    }

    void DamagedUpdate()
    {
        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (monsterBase.IsDamaged)
        {
            ChangeState(MonsterState.Damaged);
        }
        if (attackBase.canAttack && attackBase.isAttackEnd)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (moveBase.CanMove)
        {
            ChangeState(MonsterState.Move);
        }

    }

    void DeadUpdate()
    {
        monsterBase.Dead();
    }
}
