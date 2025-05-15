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
        "고대의 수정술이 깃든 마법.\n시전자 주변에 결정꽃이 피어나며, 일정 시간동안 폭발하며 수정 파편을 흩뿌린다.\n\n폭발은 넓은 범위에 마법 피해를 주며, 파편에 맞은 적은 일시적으로 마법 저항력이 감소한다.\n\n“낡은 마술사들의 유적 깊숙한 곳,\n수정을 꽃 피운 자는 존재의 경계를 넘었다고 전해진다.”",
        "두 번 연속으로 빠르게 베어내는 검술 기술.\n발동 시 사용자는 전방으로 짧게 두 차례의 연속 베기를 시전한다.\n\n첫 번째 베기는 적의 방어 자세를 흔들며,\n두 번째 베기는 틈을 파고들어 적에게 치명상을 입힌다.\n\n“그는 두 번 베었다.\n모두가 한 번 봤을 뿐인데.”",
        "고결한 신의 권능이 깃든 전투 기술.\n무기를 머리 위로 높이 들어올린 후, 빛나는 망치를 내리꽂는다.\n타격 지점에서 신성 폭발이 일어나며 넓은 범위에 신성 피해를 준다.\n\n“정의는 철과 신념으로 내려앉는다.\n죄인은 피하지 못하리라.”",
        "고대 혈의술에서 유래한 금기 마법.\n사용자는 자신의 생혈을 희생하여, 압축된 혈류 폭발을 방사한다.\n맞은 적은 짧은 시간 동안 출혈상태에 빠지게된다.\n\n“피는 생명이다.\n생명을 불태워 적을 삼킨다.”",
        "대빙기의 잔재를 불러오는 극한의 냉기 마법.\n시전자는 하늘에서 얼음폭풍을 소환하여 넓은 지역에 지속 냉기 피해를 입힌다.\n적은 서서히 얼어붙으며, 냉기 누적 시 이동 속도와 행동 속도가 감소한다.\n\n마법이 끝난 후, 얼어붙은 땅에는 일정 시간 빙결의 장이 남아,\n해당 지역을 지나는 적은 이동속도가 크게 낮아진다.\n\n“세상은 한때 얼어붙었고,\n그 고요함 속에서 신들마저 침묵했다.”",
        "오염된 연금술 지식을 응용한 저급하지만 치명적인 기술.\n사용자는 손바닥을 펼쳐 맹독성 연기를 내뿜어,\n넓은 지역을 서서히 퍼지는 독 구름으로 뒤덮는다.\n\n가스에 닿은 적은 지속 독 피해를 입으며,\n장시간 노출 시 중독 상태가 되어 속도가 감소된다.\n\n“누군가의 숨통을 조이기엔,\n칼보다 안개가 더 적합하다.”",
        "초심자도 사용하는 기초 화염 마법.\n손바닥에 작은 화염을 응축시켜, 일정 거리까지 투척한다.\n명중 시 폭발하며, 범위 화염 피해를 입힌다.\n\n장작불처럼 단순하지만, 그 위력은 사용자의 숙련도에 따라 확장될 수 있다.\n\n“화염은 언제나 시작이었다.\n파멸의, 그리고 구원의.”",
        "“봄바르딜로 꼬끼오딜로”",
        "혼돈의 회오리를 일으키는 강력한 기술.\n시전자는 마력을 집중시켜 공간을 비트는 회오리를 생성하며,\n적을 끌어당기고 중심에서 강력한 폭발을 일으킨다.\n\n회오리에 휘말린 적은 밀려나고,\n폭발 피해는 마법 또는 물리 속성으로 결정되며,\n사용 무기나 스탯에 따라 효과가 달라진다.\n\n“세상은 한 점의 회오리로 수렴한다.\n그 끝에서 모든 것이 찢겨 나가리니.”",
        "시전자 주변에 지속적으로 회전하는 화염의 고리를 생성하는 마법.\n불꽃은 무한한 시간 동안 몸을 감싸며 회전하고,\n접촉하는 적에게 지속 화염 피해와 경미한 경직 효과를 부여한다.\n\n고리는 움직이며 따라오고,적의 접근을 억제하는 방어형 마법으로도 활용 가능하다.\n\n“그는 불꽃을 입었다.\n죽음조차 그를 껴안을 수 없게 하려고.”",
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
        touchImage.SetActive(false);
    }
}
