using TMPro;
using UnityEngine;

public class CardButton : UiButton
{
    private TMP_Text title;
    private TMP_Text info;

    private int cardNumber;

    private string[] skill =
    {
        "������Ʈ �̸�",
        "Crystalize",
        "DoubleSlash",
        "PaladinHammer",
        "BloodBlast",
        "IceAge",
        "PoisonGas",
        "Fireball",
        "Kaboom",
        "Vortex",
        "EldenRing",

    };

    private string[] eventName =
    {
        "��ų �޼��� �̸�",
        "Crystalize_Skill()",
        "DoubleSlash_Skill()",
        "PaladinHammer_Skill()",
        "BloodBlast_Skill()",
        "IceAge_Skill()",
        "PoisonGas_Skill()",
        "Fireball_Skill()",
        "Kaboom_Skill()",
        "Vortex_Skill()",
        "EldenRing_Skill()",
    };

    private string[] skillInfo =
    {
        "��ų ����",
        "������ ��������� ���� ����� ��,\n������ �� �ǿ� �ڴ� ������ ��踦 �Ѿ��ٰ� ��������.��",
        "���״� �� �� ������.\n��ΰ� �� �� ���� ���ε�.��",
        "�����Ǵ� ö�� �ų����� �����ɴ´�.\n������ ������ ���ϸ���.��",
        "���Ǵ� �����̴�.\n������ ���¿� ���� ��Ų��.��",
        "�������� �Ѷ� ���پ���,\n�� ����� �ӿ��� �ŵ鸶�� ħ���ߴ�.��",
        "���������� ������ ���̱⿣,\nĮ���� �Ȱ��� �� �����ϴ�.��",
        "��ȭ���� ������ �����̾���.\n�ĸ���, �׸��� ������.��",
        "�����ٸ����� ���������Ρ�",
        "�������� �� ���� ȸ������ �����Ѵ�.\n�� ������ ��� ���� ���� ��������.��",
        "���״� �Ҳ��� �Ծ���.\n�������� �׸� ������ �� ���� �Ϸ���.��",
    };

    protected override void Awake()
    {
        base.Awake();
        GameManager.gameEvent.Add(SetSkill, true);
    }

    private void SetSkill()
    {
        cardNumber = Random.Range(0, skill.Length);
        title.text = skill[cardNumber];
        info.text = skillInfo[cardNumber];
    }

    protected override void Click()
    {
        //GameManager.gameEvent.Call(skill[cardNumber]);
        GameManager.gameEvent.Call("CarWindowOff");
    }
}
