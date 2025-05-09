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

    public void ChangeState(MonsterState newState)
    {
        ExitState(currentState);

        currentState = newState;

        EnterState(currentState);
    }

    private void EnterState(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                IdleEnter();
                break;
            case MonsterState.Move:
                MoveEnter();
                break;
            case MonsterState.Attack:
                AttackEnter();
                break;
        }
    }

    private void ExitState(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Idle:
                IdleExit();
                break;
            case MonsterState.Move:
                MoveExit();
                break;
            case MonsterState.Attack:
                AttackExit();
                break;
        }
    }

    // Enter
    void IdleEnter()
    {
        

    }
    
    void MoveEnter()
    {

    }

    void AttackEnter() 
    {
        
    }

    void DeadEnter()
    { 
    
    }
   
    //Exit
    void IdleExit()
    {

    }

    void MoveExit()
    {

    }

    void AttackExit()
    {

    }

    void DeadExit()
    {

    }

    //Update
    void IdleUpdate()
    {
        bool isMove = moveBase.IsMove;
        bool isAttack = attackBase.IsAttack;

        if (isAttack && !isMove)
        {
            ChangeState(MonsterState.Attack);
        }
        else if (!isAttack && isMove)
        {
            ChangeState(MonsterState.Move);
        }
    }

    void MoveUpdate()
    {

    }

    void AttackUpdate()
    {

    }

    void DeadUpdate()
    {

    }

}
