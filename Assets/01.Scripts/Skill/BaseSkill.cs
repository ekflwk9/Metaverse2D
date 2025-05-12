using UnityEngine;

public enum Skill_location
{
    Player,
    CloseEnemy,
    FarEnemy,
}

public abstract class BaseSkill : MonoBehaviour
{
    protected delegate Vector3 Coordinate();
    protected Coordinate del_location;

    protected Animator anim;
    protected Rigidbody2D rigid;
    protected Collider2D _collider;

    protected int count = 0;
    protected int randomState;
    protected float skillDamage;
    protected bool isPosFixed = false;

    [SerializeField] protected int getDmg;
    [SerializeField] protected int skillCooldown;
    [SerializeField] protected float skillSpeed = 0f;
    [SerializeField] protected float forward = 0.5f;

    protected Vector3 generateLocation = Vector3.zero;
    protected Vector3 direction = Vector3.zero;

    protected void Awake()
    {
        GetSkill();
        //Test ���ؼ� ������
        //GameManager.gameEvent.Add(GetSkill, true);

        _collider = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //�� ������ ������ �Ф�
        DontDestroyOnLoad(gameObject);

        //��ų �̸� �������ִ°� ���� �ִϸ��̼ǿ��� ���� �� ����
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

    protected virtual void Algorithm()
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
            isPosFixed = true;
            CoordinateOfSkill();
        }
    }

    //��ų �ߵ��� �߻�
    //����� ��ų�� ��� ����
    protected virtual void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - GameManager.player.transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    //��ų �ߵ� ���� �� ��ġ
    protected void CoordinateOfSkill()
    {
        if (del_location != null)
        {
            generateLocation = del_location.Invoke();
            transform.position = generateLocation;
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

    //�� �״�� ���� ����� �� ���ӿ�����Ʈ�� ã�� �޼���
    private GameObject FindCloseEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closeEnemy = null;

        float minDistance = Mathf.Infinity;
        Vector3 playerPos = GameManager.player.transform.position;

        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
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

    //��������Ʈ ��Ʈ�ѷ� ��ų�� �ߵ���Ű�� ���� ��ġ�� Player �� Enemy�� �Է�
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

    //��ų ȹ��� �÷��̾� ������ ����
    protected virtual void DmgChange()
    {
        GameManager.player.StateUp(StateCode.Damage, getDmg);
    }

    //��ų�� ������ = �÷��̾� �������� 1.5�� ~ 2��
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
            //DirectionOfProjectileSkill(collision.transform.position);

            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    //��ų Collider2D �� �ִϸ��̼� �̺�Ʈ�� �۵���Ű�� ���� �޼���
    protected virtual void EnableCollider()
    {
        _collider.enabled = true;
    }

    protected virtual void DisableCollider()
    {
        _collider.enabled = false;
    }

    //��ų ���� �޼���
    //�ִϸ��̼��� ���� Add Event�� �� �޼��带 �߰��ϸ� �ȴ�.
    protected virtual void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}