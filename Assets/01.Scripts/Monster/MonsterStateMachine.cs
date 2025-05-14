using System.Collections;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Attack
}

public class MonsterStateMachine : MonoBehaviour
{
    private MonsterState currentState;

    private MonsterMoveBase moveBase;
    private MonsterAttackBase attackBase;
    private MonsterBase monsterBase;

    void Awake()
    {
        moveBase = GetComponent<MonsterMoveBase>();
        attackBase = GetComponent<MonsterAttackBase>();
        monsterBase = GetComponent<MonsterBase>();
    }

    void Start()
    {
        ChangeState(MonsterState.Idle);
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
                // 대기 (공격 중엔 별도 로직 필요 없음)
                break;
        }
    }

    void ChangeState(MonsterState newState)
    {
        if (currentState == newState) return;

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

            case MonsterState.Move:
                monsterBase.animator.SetBool("isMoving", true);
                break;

            case MonsterState.Attack:
                monsterBase.animator.SetBool("isMoving", false);
                monsterBase.animator.SetTrigger("isAttacking");

                moveBase.StopMove();
                attackBase.StartAttack(OnAttackEnd);
                break;
        }
    }

    void IdleUpdate()
    {
        monsterBase.FlipMainSprite();

        if (monsterBase.IsDead)
        {
            monsterBase.Dead();
            enabled = false; // 상태머신 멈춤
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.player.transform.position);

        if (attackBase.CanPerformAttack() && distanceToPlayer <= monsterBase.attackRange)
            ChangeState(MonsterState.Attack);
        else if (moveBase.CanMove)
            ChangeState(MonsterState.Move);
    }

    void MoveUpdate()
    {
        monsterBase.FlipMainSprite();
        moveBase.OnMove();

        if (monsterBase.IsDead)
        {
            monsterBase.Dead();
            enabled = false;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, GameManager.player.transform.position);

        if (attackBase.CanPerformAttack() && distanceToPlayer <= monsterBase.attackRange)
            ChangeState(MonsterState.Attack);
    }

    void OnAttackEnd()
    {
        if (monsterBase.IsDead)
        {
            monsterBase.Dead();
            enabled = false;
            return;
        }

        if (moveBase.CanMove)
            ChangeState(MonsterState.Move);
        else
            ChangeState(MonsterState.Idle);
    }
}
