using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    private Animator anim;
    
    private int randomState;
    private float skillDamage;
    
    private Vector2 range = Vector2.zero;
    
    private void Awake()
    {
        GameManager.gameEvent.Add(GetSkill, true);
    }

    private void GetSkill()
    {
        GameManager.player.AddSkill(SkillName);
        RangeOfSkill();
        DmgChange();
    }

    private void SkillName()
    {
        this.transform.position = GameManager.player.transform.position;
        DmgApply();
    }

    private void RangeOfSkill()
    {
        //��ų�� ����
        var pos = GameManager.player.transform.position;

        range.x = pos.x + 1f;
        range.y = pos.y + 0f;
    }

    private void DmgChange()
    {
        //��ų ȹ��� �÷��̾� ������ ����
        GameManager.player.StateUp(StateCode.Damage, 0);
    }

    private void DmgApply()
    {
        //��ų�� ������ = �÷��̾� �������� 1.5�� ~ 2��
        randomState = Random.Range(5, 11);
        skillDamage = ( randomState * 0.1f ) + GameManager.player.dmg;
    }

    //�� �ִ°�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }
}
