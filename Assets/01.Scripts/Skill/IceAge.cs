using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAge : BaseSkill
{
    public override void GetSkill()
    {
        GameManager.player.AddSkill(IceAge_Skill);
        DmgChange();
    }

    protected void IceAge_Skill()
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

    protected override void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - GameManager.player.transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    protected override void CoordinateOfSkill()
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

    protected override void DmgChange()
    {
        //스킬 획득시 플레이어 데미지 조정
        GameManager.player.StateUp(StateCode.Damage, 0);
    }

    protected override void SkillDmg()
    {
        //스킬의 데미지 = 플레이어 데미지의 1.5배 ~ 2배
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    //딜 넣는거
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DirectionOfProjectileSkill(collision.transform.position);

            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    protected override void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}
