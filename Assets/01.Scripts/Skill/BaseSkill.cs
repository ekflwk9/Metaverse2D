using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected Collider2D _collider;

    protected int count = 0;
    protected int randomState;
    protected float skillDamage;

    [SerializeField] protected float skillSpeed = 0f;
    [SerializeField] protected float forward = 0.5f;

    protected Vector3 generateLocation = Vector3.zero;
    protected Vector3 direction = Vector3.zero;

    //Test 위해 수정함
    //protected void Awake()
    //{
    //    GameManager.gameEvent.Add(GetSkill, true);
    //    DontDestroyOnLoad(gameObject);
    //}

    public virtual void GetSkill()
    {
        GameManager.player.AddSkill(SkillName);
        DmgChange();
    }

    protected virtual void SkillName()
    {
        count++;
        if (count >= 6)
        {
            this.gameObject.SetActive(true);
            SkillDmg();
            CoordinateOfSkill();
            count = 0;
        }
    }

    protected virtual void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - GameManager.player.transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    protected virtual void CoordinateOfSkill()
    {
        direction = GameManager.player.direction;
        var pos = GameManager.player.transform.position;

        if (direction.x >= 0)
        {
            generateLocation.x = pos.x + forward;
        }
        else
        {
            generateLocation.x = pos.x - forward;
        }

        if (direction.y >= 0)
        {
            generateLocation.y = pos.y + forward;
        }
        else
        {
            generateLocation.y = pos.y - forward;
        }
        
        this.transform.position += generateLocation;
    }

    protected virtual void DmgChange()
    {
        //스킬 획득시 플레이어 데미지 조정
        GameManager.player.StateUp(StateCode.Damage, 0);
    }

    protected virtual void SkillDmg()
    {
        //스킬의 데미지 = 플레이어 데미지의 1.5배 ~ 2배
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    //딜 넣는거
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //DirectionOfProjectileSkill(collision.transform.position);

            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    protected virtual void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}