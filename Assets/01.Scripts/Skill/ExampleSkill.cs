using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ExampleSkill : MonoBehaviour
{
    private Animator anim;
    
    private int count = 0;
    private int randomState;
    private float skillDamage;
    
    private Vector2 range = Vector2.zero;
    
    private void Awake()
    {
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetSkill();
        }

    }

    private void GetSkill()
    {
        GameManager.player.AddSkill(SkillName);
        DmgChange();
    }

    private void SkillName()
    {
        count++;
        if (count >= 6)
        {

            this.gameObject.SetActive(true);
            DmgApply();
            RangeOfSkill();
            count = 0;
        }

    }

    private void RangeOfSkill()
    {
        //스킬의 범위
        var pos = GameManager.player.transform.position;

        
        range.x = pos.x + 0f;
        range.y = pos.y + 0f;

        this.transform.position = range;
    }

    private void DmgChange()
    {
        //스킬 획득시 플레이어 데미지 조정
        GameManager.player.StateUp(StateCode.Damage, 0);
    }

    private void DmgApply()
    {
        //스킬의 데미지 = 플레이어 데미지의 1.5배 ~ 2배
        randomState = Random.Range(5, 11);
        skillDamage = ( randomState * 0.1f ) + GameManager.player.dmg;
    }

    //딜 넣는거
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    private void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}
