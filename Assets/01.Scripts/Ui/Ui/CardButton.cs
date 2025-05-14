using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : UiButton
{
    private TMP_Text title;
    private TMP_Text info;
    private Image icon;

    private int cardNumber;

    private string[] skill =
    {
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

    private string[] skillInfo =
    {
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
        title = Service.FindChild(this.transform, "Title").GetComponent<TMP_Text>();
        info = Service.FindChild(this.transform, "Info").GetComponent<TMP_Text>();
        icon = Service.FindChild(this.transform, "Icon").GetComponent<Image>();

        GameManager.gameEvent.Add(SetSkill, true);
    }

    private void SetSkill()
    {
        cardNumber = Random.Range(0, skill.Length);
        title.text = skill[cardNumber];
        info.text = skillInfo[cardNumber];
        icon.sprite = GameManager.sprite.GetImage(skill[cardNumber]);
    }

    protected override void Click()
    {
        GameManager.gameEvent.Call($"{skill[cardNumber]}GetSkill");
        GameManager.gameEvent.Call("CardWindowOff");

        GameManager.gameEvent.Call("MapManagerManualClear");
    }
}
