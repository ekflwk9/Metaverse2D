using Unity.VisualScripting;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Attack,
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

            case MonsterState.Dead:
                DeadUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        monsterBase.Idle();

        if (moveBase.CanMove && !monsterBase.IsDead)
        {
            ChangeState(MonsterState.Move);
        }
        else if (attackBase.CanAttack && !monsterBase.IsDead)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
    }

    void MoveUpdate()
    {
        moveBase.Move();

        if (attackBase.CanAttack && !monsterBase.IsDead)
        {
            ChangeState(MonsterState.Attack);
        }
        else if(monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (!attackBase.CanAttack && !moveBase.CanMove && !monsterBase.IsDead)
        {
            ChangeState(MonsterState.Idle);
        }

    }

    void AttackUpdate()
    {
        attackBase.Attack();
        if (!attackBase.CanAttack && moveBase.CanMove)
        {
            ChangeState(MonsterState.Move);
        }
        else if (monsterBase.IsDead)
        {
            ChangeState(MonsterState.Dead);
        }
        else if (!attackBase.CanAttack && !moveBase.CanMove && !monsterBase.IsDead)
        {
            ChangeState(MonsterState.Idle);
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
