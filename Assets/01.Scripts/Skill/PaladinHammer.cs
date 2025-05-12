using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinHammer : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 2;
    //private int skillCooldown = 6;
    //private float skillSpeed = 0f;
    //private float forward = 1.5f;

    public override void GetSkill()
    {
        //Test용 코드
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(gameObject);

        GameManager.player.AddSkill(PaladinHammer_Skill);

        DmgChange();
        SkillLocation(Skill_location.FarEnemy);
    }

    protected void PaladinHammer_Skill()
    {
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            SkillDmg();
            count = 0;
            isPosFixed = false;
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            CoordinateOfSkill();
        }
    }

    protected override void DmgChange()
    {
        GameManager.player.StateUp(StateCode.Damage, getDmg);
    }

    protected override void SkillDmg()
    {
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }
}
