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
        GameManager.player.AddSkill(BloodBlast_Skill);

        SkillLocation(Skill_location.CloseEnemy);
        DmgChange();
    }

    protected void BloodBlast_Skill()
    {
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            count = 0;
            isPosFixed = false;
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            LocationOfSkill();
        }
    }
}
