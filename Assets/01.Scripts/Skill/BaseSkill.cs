using UnityEngine;

public enum Skill_location
{
    Player,
    CloseEnemy,
    FarEnemy,
}

public abstract class BaseSkill : MonoBehaviour
{
    protected delegate Vector3 Location();
    protected Location del_location;

    protected Animator anim;
    protected Rigidbody2D rigid;
    protected Collider2D _collider;
    protected ParticleSystem particle;

    protected DmgTypeCode dmgType;

    protected int count = 0;
    protected int randomState;
    protected float skillDamage;
    protected bool isPosFixed = false;

    [SerializeField] protected int getDmg;
    [SerializeField] protected int skillCooldown;
    [SerializeField] protected float changeDamage;
    [SerializeField] protected float forward;
    [SerializeField] protected float skillSpeed;
    [SerializeField] protected float slowAmount;

    protected Vector3 direction = Vector3.zero;
    protected Vector3 generateLocation = Vector3.zero;

    public void SetSkill()
    {
        GameManager.gameEvent.Add(GetSkill, true);

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        particle = GetComponentInChildren<ParticleSystem>();

        DontDestroyOnLoad(gameObject);

        _collider.enabled = false;

        //스킬 생성 위치
        //SkillLocation(Skill_location.Player);
    }

    //호출시 스킬을 이벤트에 추가
    public virtual void GetSkill()
    {
        GameManager.player.AddSkill(Algorithm);
        DmgChange();
    }

    private void Algorithm()
    { 
        //count++ 되는동안 스킬이 해당위치에 고정
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            count = 0;
            isPosFixed = false;
        }

        // 위치는 한 번만 고정
        if (!isPosFixed)
        {
            LocationOfSkill();
            isPosFixed = true;
        }
    }

    //스킬 발사 방향 및 속도
    protected virtual void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    /// <summary>
    /// 스킬 발동 방향 및 위치 (델리게이트)
    /// </summary>
    protected void LocationOfSkill()
    {
        if (del_location != null)
        {
            generateLocation = del_location.Invoke();
            transform.position = generateLocation;
        }
    }

    /// <summary>
    /// 델리게이트 컨트롤러. 스킬을 발동시키고 싶은 위치
    /// Player CloseEnemy FarEnemy
    /// </summary>
    /// <param name="locationType"></param>
    protected void SkillLocation(Skill_location locationType)
    {
        switch (locationType)
        {
            case Skill_location.Player:
                del_location = PlayerPosition;
                break;

            case Skill_location.CloseEnemy:
                del_location = EnemyClosePosition;
                break;

            case Skill_location.FarEnemy:
                del_location = EnemyFarPosition;
                break;
        }
    }

    //델리게이트용 플레이어 위치에서 생성될 스킬의 벡터
    protected Vector3 PlayerPosition()
    {
        direction = GameManager.player.direction;
        var pos = GameManager.player.transform.position;
        Vector3 result = pos;

        if (direction.x > 0) result.x += forward;
        else if (direction.x < 0) result.x -= forward;

        if (direction.y > 0) result.y += forward;
        else if (direction.y < 0) result.y -= forward;

        return result;
    }

    //델리게이트용 적 위치에서 생성될 스킬의 벡터
    protected Vector3 EnemyClosePosition()
    {
        GameObject nearestEnemy = FindCloseEnemy();

        if (nearestEnemy != null)
        {
            return nearestEnemy.transform.position;
        }

        return GameManager.player.transform.position;
    }

    protected Vector3 EnemyFarPosition()
    {
        GameObject farestEnemy = FindFarEnemy();

        if (farestEnemy != null)
        {
            return farestEnemy.transform.position;
        }

        return GameManager.player.transform.position;
    }

    /// <summary>
    /// 가장 가까운 적 게임오브젝트를 찾는 메서드
    /// </summary>
    /// <returns></returns>
    private GameObject FindCloseEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closeEnemy = null;

        float minDistance = float.MaxValue;
        Vector3 playerPos = GameManager.player.transform.position;

        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies) //수정하기
            {
                float _dist = Vector3.Distance(playerPos, enemy.transform.position);
                if (_dist < minDistance)
                {
                    minDistance = _dist;
                    closeEnemy = enemy;
                }
            }
        }

        return closeEnemy;
    }

    /// <summary>
    /// 가장 멀리있는 적 게임오브젝트를 찾는 메서드
    /// </summary>
    /// <returns></returns>
    private GameObject FindFarEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject farEnemy = null;

        float maxDistance = 0f;
        Vector3 playerPos = GameManager.player.transform.position;

        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float _dist = Vector3.Distance(playerPos, enemy.transform.position);
                if (_dist > maxDistance)
                {
                    maxDistance = _dist;
                    farEnemy = enemy;
                }
            }
        }

        return farEnemy;
    }

    /// <summary>
    /// 스킬 획득시 플레이어 데미지 조정
    /// </summary>
    protected virtual void DmgChange()
    {
        GameManager.player.StateUp(StateCode.Damage, getDmg);
    }

    /// <summary>
    /// 스킬의 데미지 = 플레이어 데미지의 1.5배 ~ 2배
    /// </summary>
    protected virtual void SkillDmg()
    {
        float baseDmg = GameManager.player.dmg * changeDamage;
        float random = Random.Range(-0.3f, 0.3f);
        skillDamage = baseDmg * (1f + random);

        if (Random.value < 0.25f)
        {
            skillDamage *= 1.5f;
            dmgType = DmgTypeCode.CriticalDamage;
        }
        else
        {
            dmgType = DmgTypeCode.Damage;
        }
    }

    //스킬 오브젝트의 Collider에 적이 감지되면
    //스킬 데미지로 적의 HP를 감소
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SkillDmg();
            CamAction();
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
            GameManager.effect.Damage(collision.transform.position + Vector3.up, x, dmgType);
        }
    }

    private void StartParticle()
    {
        if (particle != null)
        {
            particle.Play();
        }
    }

    /// <summary>
    /// 스킬 Collider2D 를 애니메이션 이벤트로 작동시키기 위한 메서드
    /// </summary>
    protected virtual void EnableCollider()
    {
        _collider.enabled = true;
    }

    /// <summary>
    /// 스킬 Collider2D 를 애니메이션 이벤트로 작동시키기 위한 메서드
    /// </summary>
    protected virtual void DisableCollider()
    {
        _collider.enabled = false;
    }

    protected void CamAction()
    {
        GameManager.cam.Action("Shake");
    }

    //스킬 종료 메서드
    //애니메이션의 끝에 Add Event로 이 메서드를 추가하면 된다.
    protected virtual void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}