using System.Collections;
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
        Service.Log($"현재 상태: {currentState}");
        switch (currentState)
        {
            case MonsterState.Idle:
                IdleUpdate();
                break;

            case MonsterState.Move:
                Service.Log("MoveUpdate 호출");
                MoveUpdate();
                break;

            case MonsterState.Attack:
                // 공격 중에는 Update에서 아무것도 안 함. 공격 종료 시 콜백으로 상태 전이
                break;

            case MonsterState.Damaged:
                DamagedUpdate();
                break;

            case MonsterState.Dead:
                // 죽었으면 아무것도 하지 않음
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
                break; // OnMove는 Update에서 실행

            case MonsterState.Attack:
                monsterBase.animator.SetBool("isMoving", false);
                monsterBase.animator.SetTrigger("isAttacking");
                Service.Log("isAttacking이 true가 맞나용?: " + monsterBase.animator.GetBool("isAttacking"));

                moveBase.StopMove();

                attackBase.StartAttack(OnAttackEnd);
                break;

            case MonsterState.Damaged:
                monsterBase.TakeDamage(GameManager.player.dmg); // 데미지 처리
                break;

            case MonsterState.Dead:
                monsterBase.Dead(); // 사망 처리
                break;
        }
    }

    void IdleUpdate()
    {
        monsterBase.FlipMainSprite();

        if (monsterBase.IsDead)
            ChangeState(MonsterState.Dead);
        else if (monsterBase.IsDamaged)
            ChangeState(MonsterState.Damaged);
        else if (attackBase.CanPerformAttack())
            ChangeState(MonsterState.Attack);
        else if (moveBase.CanMove)
            ChangeState(MonsterState.Move);
    }

    void MoveUpdate()
    {
        monsterBase.FlipMainSprite();
        moveBase.OnMove();
        monsterBase.animator.SetBool("isMoving", true);

        if (monsterBase.IsDead)
            ChangeState(MonsterState.Dead);
        else if (monsterBase.IsDamaged)
            ChangeState(MonsterState.Damaged);
        else if (
            attackBase.CanPerformAttack() &&
            Vector2.Distance(transform.position, GameManager.player.transform.position) <= monsterBase.attackRange
            )
            ChangeState(MonsterState.Attack);
    }

    void DamagedUpdate()
    {
        if (monsterBase.IsDead)
            ChangeState(MonsterState.Dead);
        else
            ChangeState(MonsterState.Idle); // 피격 후 Idle 복귀
    }

    void OnAttackEnd()
    {
        Service.Log("공격 끝!");
        
        //monsterBase.animator.SetBool("isAttacking", false);
        // 공격 애니메이션이 끝날 때까지 기다린 후 상태 전환
        StartCoroutine(WaitForAttackEnd());
    }

    private IEnumerator WaitForAttackEnd()
    {
        yield return new WaitForSeconds(attackBase.attackCooldown); // 공격 애니메이션이 끝날 때까지 대기

        if (monsterBase.IsDead)
        {
            Service.Log("Dead 상태로 전환");
            ChangeState(MonsterState.Dead);
        }
        else if (monsterBase.IsDamaged)
        {
            Service.Log("Damaged 상태로 전환");
            ChangeState(MonsterState.Damaged);
        }
        else if (moveBase.CanMove)
        {
            Service.Log("Move 상태로 전환");
            ChangeState(MonsterState.Move);
        }
        else
        {
            Service.Log("Idle 상태로 전환");
            ChangeState(MonsterState.Idle);
        }
    }
}
