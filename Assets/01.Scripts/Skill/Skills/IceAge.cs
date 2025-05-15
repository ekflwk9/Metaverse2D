using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAge : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 15;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
        GameManager.player.AddSkill(IceAge_Skill);

        SkillLocation(Skill_location.CloseEnemy);
        DmgChange();
    }

    protected void IceAge_Skill()
    {
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            FlipSkill();
            count = 0;
            isPosFixed = false;
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            LocationOfSkill();
        }
    }

    private void FlipSkill()
    {
        var pos = EnemyClosePosition();
        var original = Vector3.one;
        var sca = transform.localScale;

        if (pos.x < GameManager.player.transform.position.x)
            sca.x = -original.x;
        else sca.x = original.x;
            //var flip = pos.x > GameManager.player.transform.position.x ? 1 : -1;
            //sca.x = flip
            transform.localScale = sca;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SkillDmg();
            CamAction();
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
            GameManager.effect.Damage(collision.transform.position + Vector3.up, x, DmgTypeCode.CriticalDamage);
            //collision.gameObject.GetComponent<MonsterBase>().ApplySlow(slowAmount);
        }
    }
    //[SerializeField] Vector3 size;
    //private void OnDrawGizmos()
    //{

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(this.transform.position, size);
    //}
}
