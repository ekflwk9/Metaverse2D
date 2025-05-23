using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristalize : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 10;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
        GameManager.player.AddSkill(Cristalize_Skill);

        DmgChange();
        SkillLocation(Skill_location.CloseEnemy);
    }

    protected void Cristalize_Skill()
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
