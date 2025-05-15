using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DoubleSlash : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 4;
    //private float skillSpeed = 0f;
    //private float forward = 1f;

    public override void GetSkill()
    {
        GameManager.player.AddSkill(DoubleSlash_Skill);

        DmgChange();
        SkillLocation(Skill_location.Player);
    }

    protected void DoubleSlash_Skill()
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
            RotateSkill();
        }
    }

    private void RotateSkill()
    {
        if (GameManager.player.direction != Vector3.zero)
        {
            //transform.LookAt 알아보기 숙제
            Vector3 dir = GameManager.player.direction;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
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
