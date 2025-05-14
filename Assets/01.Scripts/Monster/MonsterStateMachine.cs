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
                break;

            case MonsterState.Attack:
                monsterBase.animator.SetBool("isMoving", false);
                monsterBase.animator.SetTrigger("isAttacking");
                Service.Log("isAttacking이 true가 맞나용?: " + monsterBase.animator.GetBool("isAttacking"));

                moveBase.StopMove();

                attackBase.StartAttack(OnAttackEnd);
                break;
                          
        }
    }

    void IdleUpdate()
    {
        monsterBase.FlipMainSprite();

               if (
    attackBase.CanPerformAttack() &&
    Vector2.Distance(transform.position, GameManager.player.transform.position) <= monsterBase.attackRange
)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (moveBase.CanMove)
            ChangeState(MonsterState.Move);
    }

    void MoveUpdate()
    {
        monsterBase.FlipMainSprite();
        moveBase.OnMove();
        monsterBase.animator.SetBool("isMoving", true);

        if (
            attackBase.CanPerformAttack() &&
            Vector2.Distance(transform.position, GameManager.player.transform.position) <= monsterBase.attackRange
            )
            ChangeState(MonsterState.Attack);
    }

    void OnAttackEnd()
    {
        Service.Log("공격 끝!");


        StartCoroutine(WaitForAttackEnd());
    }

    private IEnumerator WaitForAttackEnd()
    {
        yield return new WaitForSeconds(attackBase.attackCooldown);

        if (moveBase.CanMove)
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
