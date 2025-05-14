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

    protected int count = 0;
    protected int randomState;
    protected float skillDamage;
    protected bool isPosFixed = false;

    [SerializeField] protected int getDmg;
    [SerializeField] protected int skillCooldown;
    [SerializeField] protected float skillSpeed = 0f;
    [SerializeField] protected float forward = 0.5f;
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

        //��ų ���� ��ġ
        //SkillLocation(Skill_location.Player);
    }

    //ȣ��� ��ų�� �̺�Ʈ�� �߰�
    public virtual void GetSkill()
    {
        GameManager.player.AddSkill(Algorithm);
        DmgChange();
    }

    private void Algorithm()
    { 
        //count++ �Ǵµ��� ��ų�� �ش���ġ�� ����
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            SkillDmg();
            count = 0;
            isPosFixed = false;
        }

        // ��ġ�� �� ���� ����
        if (!isPosFixed)
        {
            LocationOfSkill();
            isPosFixed = true;
        }
    }

    //��ų �߻� ���� �� �ӵ�
    protected virtual void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    /// <summary>
    /// ��ų �ߵ� ���� �� ��ġ (��������Ʈ)
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
    /// ��������Ʈ ��Ʈ�ѷ�. ��ų�� �ߵ���Ű�� ���� ��ġ
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

    //��������Ʈ�� �÷��̾� ��ġ���� ������ ��ų�� ����
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

    //��������Ʈ�� �� ��ġ���� ������ ��ų�� ����
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
    /// ���� ����� �� ���ӿ�����Ʈ�� ã�� �޼���
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
            foreach (GameObject enemy in enemies) //�����ϱ�
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
    /// ���� �ָ��ִ� �� ���ӿ�����Ʈ�� ã�� �޼���
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
    /// ��ų ȹ��� �÷��̾� ������ ����
    /// </summary>
    protected virtual void DmgChange()
    {
        GameManager.player.StateUp(StateCode.Damage, getDmg);
    }

    /// <summary>
    /// ��ų�� ������ = �÷��̾� �������� 1.5�� ~ 2��
    /// </summary>
    protected virtual void SkillDmg()
    {
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    //��ų ������Ʈ�� Collider�� ���� �����Ǹ�
    //��ų �������� ���� HP�� ����
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            CamAction();
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
            GameManager.effect.Damage(collision.transform.position, x, DmgTypeCode.CriticalDamage);
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
    /// ��ų Collider2D �� �ִϸ��̼� �̺�Ʈ�� �۵���Ű�� ���� �޼���
    /// </summary>
    protected virtual void EnableCollider()
    {
        _collider.enabled = true;
    }

    /// <summary>
    /// ��ų Collider2D �� �ִϸ��̼� �̺�Ʈ�� �۵���Ű�� ���� �޼���
    /// </summary>
    protected virtual void DisableCollider()
    {
        _collider.enabled = false;
    }

    protected void CamAction()
    {
        GameManager.cam.Action("Shake");
    }

    //��ų ���� �޼���
    //�ִϸ��̼��� ���� Add Event�� �� �޼��带 �߰��ϸ� �ȴ�.
    protected virtual void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}