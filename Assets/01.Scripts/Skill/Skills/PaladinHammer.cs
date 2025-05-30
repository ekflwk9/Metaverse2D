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
