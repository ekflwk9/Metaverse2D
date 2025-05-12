using UnityEngine;

public class EldenRing : BaseSkill
{
    //조정 후 활성화
    //private int getDmg = 0;
    //private int skillCooldown = 14;
    //private float skillSpeed = 0f;
    //private float forward = 0f;

    public override void GetSkill()
    {
        //Test용 코드
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(gameObject);

        GameManager.player.AddSkill(EldenRing_Skill);

        DmgChange();
        SkillLocation(Skill_location.Player);
    }

    private void Update()
    {
        CoordinateOfSkill();
    }

    protected void EldenRing_Skill()
    {
        count++;

        if (count >= skillCooldown)
        {
            this.gameObject.SetActive(true);
            CoordinateOfSkill();
            SkillDmg();
            count = 0;
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
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }
}
