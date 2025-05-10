using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinHammer : BaseSkill
{
    private bool isPosFixed = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public override void GetSkill()
    {
        GameManager.gameEvent.Add(GetSkill, true);
        DontDestroyOnLoad(transform.root.gameObject);

        //��ų�� �÷��̾�� �߰�
        GameManager.player.AddSkill(PaladinHammer_Skill);
        //��ų�� �÷��̾� ���ݿ� ������ ���� �� ����
        //��ų�� �÷��̾� ���ݷ� ���ݿ� ����
        DmgChange();
    }

    //��ų�� ��ü�� �ɷ±���
    protected void PaladinHammer_Skill()
    {
        //count++ �Ǵµ��� ��ų�� �ش���ġ�� ����
        count++;

        if (count >= 4)
        {
            this.gameObject.SetActive(true);
            SkillDmg();
            count = 0;
            isPosFixed = false; // ���� �ߵ��� ���� �ʱ�ȭ
        }

        if (!isPosFixed)
        {
            isPosFixed = true;
            CoordinateOfSkill(); // ��ġ�� �� ���� ����
        }
    }
    
    //��ų �ߵ��� �߻��� ����
    protected override void DirectionOfProjectileSkill(Vector3 target)
    {
        direction = (target - GameManager.player.transform.position).normalized;
        rigid.velocity = direction * skillSpeed;
    }

    //��ų �ߵ� ���� �� ��ġ
    protected override void CoordinateOfSkill()
    {
        direction = GameManager.player.direction;
        var pos = GameManager.player.transform.position;

        if (direction.x > 0)
            generateLocation.x = pos.x + forward;
        else if (direction.x < 0)
            generateLocation.x = pos.x - forward;
        else
            generateLocation.x = pos.x;

        if (direction.y > 0)
            generateLocation.y = pos.y + forward;
        else if (direction.y < 0)
            generateLocation.y = pos.y - forward;
        else
            generateLocation.y = pos.y;

        transform.position = generateLocation;
    }

    protected override void DmgChange()
    {
        //��ų ȹ��� �÷��̾� ������ ����
        GameManager.player.StateUp(StateCode.Damage, 2);
    }

    protected override void SkillDmg()
    {
        //��ų�� ������ = �÷��̾� �������� 1.5�� ~ 2��
        randomState = Random.Range(5, 11);
        skillDamage = (randomState * 0.1f) + GameManager.player.dmg;
    }

    //��ų ������Ʈ�� Collider�� ���� �����Ǹ�
    //��ų �������� ���� HP�� ��´�.
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //DirectionOfSkill(collision.transform.position);

            int x = (int)skillDamage;
            GameManager.gameEvent.Hit(collision.gameObject.name, x);
        }
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }

    //��ų ���� �޼���
    //�ִϸ��̼��� ���� Add Event�� �� �޼��带 �߰��ϸ� �ȴ�.
    protected override void AnimationOff()
    {
        this.gameObject.SetActive(false);
    }
}
