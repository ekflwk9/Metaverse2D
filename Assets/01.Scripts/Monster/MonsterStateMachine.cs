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
    private Animator anim;

    void Awake()
    {
        moveBase = GetComponent<MonsterMoveBase>();
        attackBase = GetComponent<MonsterAttackBase>();
        monsterBase = GetComponent<MonsterBase>();
    }

    void Start()
    {
        currentState = MonsterState.Idle;
    }

    void Update()
    {
        Debug.Log($"[StateMachine] Current State: {currentState}");

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
    void IdleUpdate()
    {
        monsterBase.Idle();

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (attackBase.CanAttack)
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

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (!moveBase.IsMoving)
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
        attackBase.OnAttack();

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (monsterBase.IsDamaged)
        {
            ChangeState(MonsterState.Damaged);
        }
        else if (!attackBase.IsAttacking)
        {
            if (moveBase.CanMove)
            {
                ChangeState(MonsterState.Move);
            }
            else if (!moveBase.CanMove && !attackBase.CanAttack)
            {
                ChangeState(MonsterState.Idle);
            }
        }
    }

    void DamagedUpdate()
    {
        monsterBase.TakeDamage(GameManager.player.dmg);

        if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (attackBase.CanAttack)
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

    void ChangeState(MonsterState newState)
    {
        if (currentState == newState)
            return;

        previousState = currentState;

        if (previousState == MonsterState.Idle)
        {
            monsterBase.StopIdle();
        }
        else if (previousState == MonsterState.Move)
        {
            moveBase.StopMove();
        }
        else if (previousState == MonsterState.Attack)
        {
            attackBase.StopAttack();
        }
        
        currentState = newState;
    }
}
