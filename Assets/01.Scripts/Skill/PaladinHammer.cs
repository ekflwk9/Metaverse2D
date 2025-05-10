using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinHammer : BaseSkill
{
    private bool isPosFixed = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public override void GetSkill()
    {
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(transform.root.gameObject);

        //스킬을 플레이어에게 추가
        GameManager.player.AddSkill(PaladinHammer_Skill);
        //스킬이 플레이어 스텟에 관여함 증가 및 감소
        //스킬이 플레이어 공격력 스텟에 관여
        DmgChange();
    }

    //스킬의 구체적 능력구현
    protected void PaladinHammer_Skill()
    {
        //count++ 되는동안 스킬이 해당위치에 고정
        count++;

        if (count >= 4)
        {
            this.gameObject.SetActive(true);
            SkillDmg();
            count = 0;
            isPosFixed = false; // 다음 발동을 위해 초기화
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            CoordinateOfSkill(); // 위치는 한 번만 고정
        }
    }
    
    //스킬 발동시 발사할 방향
    protected override void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - GameManager.player.transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    //스킬 발동 방향 및 위치
    protected override void CoordinateOfSkill()
    {
        direction = GameManager.player.direction;
        var pos = GameManager.player.transform.position;

        if (direction.x > 0)
            generateLocation.x = pos.x + forward;
        else if (direction.x < 0)
            generateLocation.x = pos.x - forward;
        else
            generateLocation.x = pos.x;

        if (direction.y > 0)
            generateLocation.y = pos.y + forward;
        else if (direction.y < 0)
            generateLocation.y = pos.y - forward;
        else
            generateLocation.y = pos.y;

        transform.position = generateLocation;
    }

    protected override void DmgChange()
    {
        //스킬 획득시 플레이어 데미지 조정
        GameManager.player.StateUp(StateCode.Damage, 2);
    }

    protected override void SkillDmg()
    {
        //스킬의 데미지 = 플레이어 데미지의 1.5배 ~ 2배
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    //스킬 오브젝트의 Collider에 적이 감지되면
    //스킬 데미지로 적의 HP를 깎는다.
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //DirectionOfSkill(collision.transform.position);

            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    //스킬 종료 메서드
    //애니메이션의 끝에 Add Event로 이 메서드를 추가하면 된다.
    protected override void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}
