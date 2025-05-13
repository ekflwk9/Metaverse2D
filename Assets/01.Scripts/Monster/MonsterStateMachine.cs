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
    protected MeleeAttacking meleeAttacking;

    bool isAttackEnd;

    void Awake()
    {
        moveBase = GetComponent<MonsterMoveBase>();
        attackBase = GetComponent<MonsterAttackBase>();
        monsterBase = GetComponent<MonsterBase>();
        meleeAttacking = GetComponentInChildren<MeleeAttacking>();
    }

    void Start()
    {
        currentState = MonsterState.Idle;
        isAttackEnd = meleeAttacking.isAttackEnd;
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
                if (attackBase.canAttack)
                {
                    attackBase.OnAttack();
                }
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
        else if (attackBase.canAttack)
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
        else if (attackBase.canAttack)
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
        if (isAttackEnd)
        {
            attackBase.canAttack = false;
        }

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (monsterBase.IsDamaged)
        {
            ChangeState(MonsterState.Damaged);
        }
        else if (attackBase.canAttack)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (!attackBase.canAttack)
        {
            if (moveBase.CanMove)
            {
                ChangeState(MonsterState.Move);
            }
            else if (!moveBase.CanMove)
            {
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
        else if (attackBase.canAttack)
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
