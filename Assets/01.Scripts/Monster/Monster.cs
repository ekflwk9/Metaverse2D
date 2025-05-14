using UnityEngine;

public abstract class Monster : MonoBehaviour,
IHit
{
    [Header("몬스터 정보")]
    [SerializeField] protected int dmg;
    [SerializeField] protected int health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float knockback;
    [SerializeField] protected float attackRange;

    [Space(10f)]
    [Header("블러드 임펙트 스폰 위치 조정")]
    [SerializeField] protected Vector3 bloodPos;

    private int maxHealth;
    protected bool isMove = true;
    private string monsterName;
    protected Vector3 direction = Vector3.one;

    protected Transform target;
    protected Animator anim;
    protected Rigidbody2D rigid;

    public virtual void SetMonster()
    {
        maxHealth = health;
        target = GameManager.player.transform;
        monsterName = this.GetType().Name;

        anim = GetComponent<Animator>();
        if (anim == null) Service.Log($"{this.name}에 애니메이터가 존재하지 않음");

        rigid = GetComponent<Rigidbody2D>();
        if (rigid == null) Service.Log($"{this.name}에 Rigidbody2D가 존재하지 않음");

        GameManager.SetComponent(this);
    }

    protected virtual void OnIdle()
    {
        //애니메이션 호출 메서드
        isMove = true;
        anim.Play("Idle", 0, 0);
        rigid.velocity = Vector3.zero;
    }

    public virtual void OnHit(int _dmg)
    {
        health -= _dmg;

        var effectPos = this.transform.position + bloodPos;
        GameManager.effect.Show(effectPos, "Blood");
        GameManager.sound.OnEffect(monsterName);

        if (health > 0)
        {
            isMove = false;
            rigid.velocity = (target.position - this.transform.position) * -knockback;
            anim.Play("Hit", 0, 0);
        }

        else
        {
            isMove = true;
            rigid.velocity = Vector3.zero;
            anim.SetBool("Move", false);
            anim.Play("Idle", 0, 0);

            health = maxHealth;
            this.gameObject.SetActive(false);
            Service.Log($"111");
            GameManager.map.EnemyDefeated();

            GameManager.gameEvent.Call("UpDifficulty");
            GameManager.gameEvent.Call("UpKillCount");
        }
    }

    protected virtual void Update()
    {
        if (!GameManager.stopGame)
        {
            if (isMove) Move();
        }
    }

    protected virtual void Move()
    {
        if (Service.Distance(target.position, transform.position) < attackRange)
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("Move", false);

            isMove = false;
            anim.Play("Attack", 0, 0);
        }

        else
        {
            anim.SetBool("Move", true);

            //몬스터 보는 방향
            direction.x = target.position.x < transform.position.x ? -1 : 1;
            transform.localScale = direction;

            //몬스터 이동 위치
            var movePos = target.position - transform.position;
            rigid.velocity = movePos.normalized * moveSpeed;
        }
    }
}
