using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardButton : UiButton
{
    private TMP_Text title;
    private TMP_Text info;

    private int cardNumber;

    private string[] skill =
    {
        "������Ʈ �̸� + ��ų �޼��� �̸�",
    };

    private string[] skillInfo =
{
        "��ų ����",
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
