using UnityEngine;

public class Fireball : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 0;
    //private float skillSpeed = 2.5f;
    //private float forward = 0.3f;
    private bool isBoom;

    public override void GetSkill()
    {
        GameManager.player.AddSkill(Fireball_Skill);

        DmgChange();
        SkillLocation(Skill_location.Player);
    }

    private void Update()
    {
        if (!GameManager.stopGame && isBoom)
        {
            rigid.velocity = Vector3.zero;
        }
    }

    protected void Fireball_Skill()
    {
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            count = 0;
            isBoom = false;
            isPosFixed = false;
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            LocationOfSkill();
            DirectionOfProjectileSkill(EnemyClosePosition());
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isBoom)
            {
                anim.Play("Fireball2", 0, 0);
                isBoom = true;
            }
            else
            {
                SkillDmg();
                CamAction();
                int x = (int)skillDamage;
                GameManager.gameEvent.Hit(collision.gameObject.name, x);
                GameManager.effect.Damage(collision.transform.position + Vector3.up, x, DmgTypeCode.CriticalDamage);
            }
        }
    }
}