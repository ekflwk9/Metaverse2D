using UnityEngine;

public class PoisonGas : BaseSkill
{
    //���� �� Ȱ��ȭ
    //private int getDmg = 0;
    //private int skillCooldown = 17;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
        //Test�� �ڵ�
        GameManager.gameEvent.Add(GetSkill, true);
        GameManager.player.AddSkill(PoisonGas_Skill);

        SkillLocation(Skill_location.CloseEnemy);
        DmgChange();
        DontDestroyOnLoad(gameObject);
    }

    protected void PoisonGas_Skill()
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
}
