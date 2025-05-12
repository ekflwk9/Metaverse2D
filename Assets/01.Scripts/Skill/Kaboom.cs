using UnityEngine;

public class Kaboom : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 16;
    //private float skillSpeed = 1.5f;
    //private float forward = 0f;
    private bool isBoom;

    public override void GetSkill()
    {
        //Test용 코드
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(gameObject);

        GameManager.player.AddSkill(Kaboom_Skill);

        DmgChange();
        SkillLocation(Skill_location.Player);
    }

    private void Update()
    {
        if (!GameManager.stopGame && !isBoom)
        {
            DirectionOfProjectileSkill(EnemyClosePosition());
        }
        else if (!GameManager.stopGame && isBoom)
        {
            rigid.velocity = Vector3.zero;
        }

    }

    protected void Kaboom_Skill()
    {
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            SkillDmg();
            count = 0;
            isBoom = false;
            isPosFixed = false;
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            CoordinateOfSkill();
        }
    }

    protected override void DmgChange()
    {
        GameManager.player.StateUp(StateCode.Damage, getDmg);
    }

    protected override void SkillDmg()
    {
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isBoom)
            {
                anim.Play("Kaboom", 0, 0);
                isBoom = true;
            }
            else
            {
                int x = (int)skillDamage;
                GameManager.gameEvent.Hit(collision.gameObject.name, x);
            }
        }
    }
}
