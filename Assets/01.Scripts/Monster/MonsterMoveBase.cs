using UnityEngine;

public abstract class MonsterMoveBase : MonoBehaviour
{
    protected Transform player;
    protected Rigidbody2D rb;
    protected MonsterBase monsterBase;
    protected MonsterAttackBase attackBase;
    protected float moveSpeed;
    protected float keepDistance;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        monsterBase = GetComponent<MonsterBase>();
        attackBase = GetComponent<MonsterAttackBase>();
    }

    protected virtual void Start()
    {
        player = GameManager.player.transform;
        moveSpeed = monsterBase.moveSpeed;
        keepDistance = monsterBase.keepDistance;
    }

    public abstract void Move();

    public virtual void OnMove()
    {
        Service.Log("이제 Move 호출할 차례");
        Move();
    }

    public virtual void StopMove()
    {
        rb.velocity = Vector2.zero;
    }

    public virtual bool CanMove => player != null && attackBase.IsAttackEnd;
}
