using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBlast : BaseSkill
{
    //���� �� Ȱ��ȭ
    //private int getDmg = 0;
    //private int skillCooldown = 20;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
        //Test�� �ڵ�
        GameManager.gameEvent.Add(GetSkill, true);
        GameManager.player.AddSkill(BloodBlast_Skill);

        SkillLocation(Skill_location.CloseEnemy);
        DmgChange();
        DontDestroyOnLoad(gameObject);
    }

    protected void BloodBlast_Skill()
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
            LocationOfSkill();
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
