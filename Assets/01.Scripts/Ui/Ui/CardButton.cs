using TMPro;
using UnityEngine;

public class CardButton : UiButton
{
    private TMP_Text title;
    private TMP_Text info;

    private int cardNumber;

    private string[] skill =
    {
        "오브젝트 이름",
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
        "스킬 메서드 이름",
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
        "스킬 설명",
        "“낡은 마술사들의 유적 깊숙한 곳,\n수정을 꽃 피운 자는 존재의 경계를 넘었다고 전해진다.”",
        "“그는 두 번 베었다.\n모두가 한 번 봤을 뿐인데.”",
        "“정의는 철과 신념으로 내려앉는다.\n죄인은 피하지 못하리라.”",
        "“피는 생명이다.\n생명을 불태워 적을 삼킨다.”",
        "“세상은 한때 얼어붙었고,\n그 고요함 속에서 신들마저 침묵했다.”",
        "“누군가의 숨통을 조이기엔,\n칼보다 안개가 더 적합하다.”",
        "“화염은 언제나 시작이었다.\n파멸의, 그리고 구원의.”",
        "“봄바르딜로 꼬끼오딜로”",
        "“세상은 한 점의 회오리로 수렴한다.\n그 끝에서 모든 것이 찢겨 나가리니.”",
        "“그는 불꽃을 입었다.\n죽음조차 그를 껴안을 수 없게 하려고.”",
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
