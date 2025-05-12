using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : BaseSkill
{
    //���� �� Ȱ��ȭ
    //private int getDmg = 0;
    //private int skillCooldown = 8;
    //private float skillSpeed = 2f;
    //private float forward = 0.3f;

    public override void GetSkill()
    {
        //Test�� �ڵ�
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(gameObject);

        GameManager.player.AddSkill(Vortex_Skill);

        DmgChange();
        SkillLocation(Skill_location.Player);
    }

    protected void Vortex_Skill()
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
            DirectionOfProjectileSkill(EnemyClosePosition());
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
