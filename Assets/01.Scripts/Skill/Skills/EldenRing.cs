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
        LocationOfSkill();
    }

    protected void EldenRing_Skill()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
        SkillDmg();
    }
}
