using UnityEngine;

public class PoisonGas : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 17;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
            GameManager.effect.Damage(collision.transform.position + Vector3.up, x, DmgTypeCode.CriticalDamage);
            //collision.gameObject.GetComponent<MonsterBase>().ApplySlow(slowAmount);
        }
    }
}
