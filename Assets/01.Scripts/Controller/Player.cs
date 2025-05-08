using System.Collections.Generic;
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

    private event Func skill;
    private Rigidbody2D rigid;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }

    public void StateUp(StateCode _code, float _upValue)
    {
        switch (_code)
        {
            case StateCode.MoveSpeed:
                moveSpeed += _upValue;
                anim.SetFloat("MoveSpeed", moveSpeed);
                break;

            case StateCode.AttackSpeed:
                attackSpeed += _upValue;
                anim.SetFloat("AttackSpeed", attackSpeed);
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
    public void AddSkill(Func _skill) => skill += _skill;
   
    //��ų ����
    public void RemoveSkill(Func _skill) => skill -= _skill;

    //�ִϸ��̼� ȣ�� �޼��� => ����
    private void Attack() => skill?.Invoke();

    private void Update()
    {
        if(!GameManager.stopGame) Move();      
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
        if (Input.GetKey(KeyCode.A)) pos.x = -1f;
        else if (Input.GetKey(KeyCode.D)) pos.x = 1f;

        rigid.velocity = pos.normalized * moveSpeed;
    }
}