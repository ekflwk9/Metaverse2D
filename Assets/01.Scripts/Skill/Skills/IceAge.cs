using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAge : BaseSkill
{
    //���� �� Ȱ��ȭ
    //private int getDmg = 0;
    //private int skillCooldown = 15;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
        //Test�� �ڵ�
        GameManager.gameEvent.Add(GetSkill, true);
        GameManager.player.AddSkill(IceAge_Skill);

        SkillLocation(Skill_location.CloseEnemy);
        DmgChange();
        DontDestroyOnLoad(gameObject);
    }

    protected void IceAge_Skill()
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
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);

            collision.gameObject.GetComponent<MonsterBase>().ApplySlow(slowAmount);
        }
    }
}
