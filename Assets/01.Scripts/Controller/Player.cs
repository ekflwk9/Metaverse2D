using System.Text;
using UnityEngine;
using UnityEngine.UI;

public enum StateCode
{
    Damage = 0,
    Health = 1,
    MaxHealth = 2,
    MoveSpeed = 3,
    AttackSpeed = 4,
}

public class Player : MonoBehaviour,
IHit
{
    [SerializeField] private Vector3 bloodPos; 
    [SerializeField] private Vector3 textPos; 
    [SerializeField] private Image healthBarImg;

    public int dmg { get; private set; } = 100;
    public int health { get; private set; } = 100;
    public int maxHealth { get; private set; } = 100;

    public float healthRatio = 1f;  // 데미지 받을 때 체력바 줄어드는 값 저장용
    public float moveSpeed { get; private set; } = 2f;
    public float attackSpeed { get; private set; } = 1f;

    private bool inRange = false;
    private bool isPickUp = false;
    public Vector3 direction { get => fieldDirection; }
    private Vector3 fieldDirection = Vector3.zero;
    private Vector3 scale = Vector3.one;
    

    private event Func skill;
    private Rigidbody2D rigid;
    private Animator action;
    private Animator attack;
    private Transform healthBarTrans;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        attack = GetComponent<Animator>();
        action = Service.FindChild(this.transform, "Action").GetComponent<Animator>();

        GameManager.SetComponent(this);
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateHealthBar()  // 헬스바
    {
        healthRatio = (float)health / maxHealth;
        healthBarImg.fillAmount = healthRatio;
    }

    public void OnHit(int _dmg)
    {
        var playerPos = this.transform.position;
        var bloodPos = playerPos;
        var textPos = playerPos;

        bloodPos.y += -0.5f;
        textPos.y += 0.8f;

        health -= _dmg;

        GameManager.effect.Show(playerPos, "Blood");
        GameManager.effect.FloorBlood(bloodPos);
        //GameManager.effect.Damage(textPos, _dmg);
        //GameManager.sound.OnEffect("PlayerHit");
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
                Service.Log("잘못된 추가 방식입니다 매개변수를 확인해주세요.");
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
                Service.Log("잘못된 추가 방식입니다 매개변수를 확인해주세요.");
                break;
        }
    }

    //스킬 추가
    public void AddSkill(Func _skill)
    {
        skill += _skill;
    }

    //스킬 삭제
    public void RemoveSkill(Func _skill)
    {
        skill -= _skill;
    }

    //애니메이션 호출 메서드 => 공격
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
        //줍는 모션 종료
        isPickUp = false;
    }

    public void PickUp()
    {
        //줍는 모션 호출
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
        pos.z = 0;

        //상하
        if (Input.GetKey(KeyCode.W))
        {
            pos.y = 1f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            pos.y = -1f;
        }

        //좌우
        if (Input.GetKey(KeyCode.A))
        {
            pos.x = -1f;
            scale.x = 1f;
            // 벡터로 선언한 변수의 .x 값 바꾸기
        }

        else if (Input.GetKey(KeyCode.D))
        {
            pos.x = 1f;
            scale.x = -1f;
        }

        //애니메이션 재생
        if (pos.x != 0 || pos.y != 0) action.SetBool("Move", true);
        else action.SetBool("Move", false);

        if (pos != Vector3.zero)
        {
            fieldDirection = pos.normalized;
        }

        //보는 방향
        this.transform.localScale = scale;
        rigid.velocity = pos.normalized * moveSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) inRange = false;
    }
}