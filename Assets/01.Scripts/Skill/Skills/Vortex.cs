using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 8;
    //private float skillSpeed = 2f;
    //private float forward = 0.3f;

    public override void GetSkill()
    {
        //Test용 코드
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
            LocationOfSkill();
            DirectionOfProjectileSkill(GameManager.player.direction);
        }
    }

    protected override void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = target;
        rigid.velocity = direction * skillSpeed;
    }
}
