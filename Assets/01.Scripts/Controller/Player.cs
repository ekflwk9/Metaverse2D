using UnityEngine;

public enum StateCode
{
    Damage = 0,
    Health = 1,
    MaxHealth = 2,
    MoveSpeed = 3,
    AttackSpeed = 4,
}

public class Player : MonoBehaviour
{
    public int dmg { get; private set; } = 1;
    public int health { get; private set; } = 10;
    public int maxHealth { get; private set; } = 10;
    public float moveSpeed { get; private set; } = 2f;
    public float attackSpeed { get; private set; } = 1f;

    private bool inRange = false;
    private bool isPickUp = false;
    private Vector3 direction = Vector3.one;

    private event Func skill;
    private Rigidbody2D rigid;
    private Animator action;
    private Animator attack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        attack = GetComponent<Animator>();
        action = Service.FindChild(this.transform, "Action").GetComponent<Animator>();

        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }

    public void StateUp(StateCode _code, float _upValue)
    {
        switch (_code)
        {
            case StateCode.MoveSpeed:
                moveSpeed += _upValue;
                action.SetFloat("MoveSpeed", moveSpeed);
                break;

            case StateCode.AttackSpeed:
                attackSpeed += _upValue;
                break;

            default:
                Debug.Log("�߸��� �߰� ����Դϴ� �Ű������� Ȯ�����ּ���.");
                break;
        }
    }

    public void StateUp(StateCode _code, int _upValue)
    {
        switch (_code)
        {
            case StateCode.Damage:
                dmg += _upValue;
                break;

            case StateCode.Health:
                health += _upValue;
                break;

            case StateCode.MaxHealth:
                maxHealth += _upValue;
                break;

            default:
                Debug.Log("�߸��� �߰� ����Դϴ� �Ű������� Ȯ�����ּ���.");
                break;
        }
    }

    //��ų �߰�
    public void AddSkill(Func _skill)
    {
        skill += _skill;
    }

    //��ų ����
    public void RemoveSkill(Func _skill)
    {
        skill -= _skill;
    }

    //�ִϸ��̼� ȣ�� �޼��� => ����
    private void AttackFunction()
    {
        if (inRange) skill?.Invoke();
    }

    private void Update()
    {
        if (!GameManager.stopGame && !isPickUp)
        {
            Move();
            Attack();
        }
    }

    private void EndPickUp()
    {
        //�ݴ� ��� ����
        isPickUp = false;
    }

    public void PickUp()
    {
        //�ݴ� ��� ȣ��
        action.Play("PickUp", 0, 0);
        isPickUp = true;
        rigid.velocity = Vector3.zero;
    }

    private void Attack()
    {
        if (inRange) attack.SetFloat("AttackSpeed", attackSpeed);
        else attack.SetFloat("AttackSpeed", 0);
    }

    private void Move()
    {
        var pos = this.transform.position;
        pos.x = 0;
        pos.y = 0;

        //����
        if (Input.GetKey(KeyCode.W)) pos.y = 1f;
        else if (Input.GetKey(KeyCode.S)) pos.y = -1f;

        //�¿�
        if (Input.GetKey(KeyCode.A))
        {
            pos.x = -1f;
            direction.x = 1f;
        }

        else if (Input.GetKey(KeyCode.D))
        { 
            pos.x = 1f;
            direction.x = -1f;
        }

        //�ִϸ��̼� ���
        if (pos.x != 0 || pos.y != 0) action.SetBool("Move", true);
        else action.SetBool("Move", false);

        //���� ����
        this.transform.localScale = direction;
        rigid.velocity = pos.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) inRange = false;
    }
}