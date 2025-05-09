using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rigid;

    protected int count = 0;
    protected int randomState;
    protected float skillDamage;
    protected float skillSpeed;

    protected Vector2 range = Vector2.zero;
    protected Vector3 direction = Vector3.zero;

    protected void Awake()
    {
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void GetSkill()
    {
        GameManager.player.AddSkill(SkillName);
        DmgChange();
    }

    protected virtual void SkillName()
    {
        count++;
        if (count >= 6)
        {
            this.gameObject.SetActive(true);
            DmgApply();
            CoordinateOfSkill();
            count = 0;
        }
    }

    protected virtual void DirectionOfSkill()
    {
        direction = GameManager.player.direction;
        rigid.velocity = direction.normalized * Time.deltaTime * skillSpeed;
    }

    protected virtual void CoordinateOfSkill()
    {
        //스킬의 범위
        var pos = GameManager.player.transform.position;

        range.x = pos.x + 0f;
        range.y = pos.y + 0f;

        this.transform.position = range;
    }

    protected virtual void DmgChange()
    {
        //스킬 획득시 플레이어 데미지 조정
        GameManager.player.StateUp(StateCode.Damage, 0);
    }

    protected virtual void DmgApply()
    {
        //스킬의 데미지 = 플레이어 데미지의 1.5배 ~ 2배
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    //딜 넣는거
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    protected virtual void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}