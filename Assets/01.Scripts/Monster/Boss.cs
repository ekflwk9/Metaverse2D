using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    private int count;
    private Bullet[] bullet;

    public override void OnHit(int _dmg)
    {
        health -= _dmg;

        var effectPos = this.transform.position + bloodPos;
        GameManager.effect.Show(effectPos, "Blood");

        if (health > 0)
        {
            isMove = false;
            rigid.velocity = (target.position - this.transform.position) * -knockback;
            anim.Play("Hit", 0, 0);
        }

        else
        {
            isMove = true;
            rigid.velocity = Vector3.zero;
            anim.SetBool("Move", false);
            anim.Play("Idle", 0, 0);

            this.gameObject.SetActive(false);
            GameManager.map.EnemyDefeated();
        }
    }

    public override void SetMonster()
    {
        base.SetMonster();

        var bulletResource = Service.FindResource("Enemy", "Boss_Projectile");
        bullet = new Bullet[5];

        for (int i = 0; i < bullet.Length; i++)
        {
            var spawnBullet = Instantiate(bulletResource);
            var bulletInfo = spawnBullet.GetComponent<Bullet>();

            bulletInfo.SetBullet("Player", 10);
            bullet[i] = bulletInfo;
        }
    }

    private void RangedAttack()
    {
        count++;
        if (count >= bullet.Length) count = 0;

        var thisPos = this.transform.position;
        var targetPos = GameManager.player.transform.position;

        bullet[count].Fire(thisPos, targetPos);
        bullet[count].SetRotate(thisPos, targetPos);
    }

    private void Attack()
    {
        if (Service.Distance(target.position, transform.position) < 1.8f)
        {
            GameManager.gameEvent.Hit("Player", dmg);
        }
    }

    protected override void Move()
    {
        if (Service.Distance(target.position, transform.position) < attackRange)
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("Move", false);

            isMove = false;

            var ranAttack = Random.Range(0, 2);
            anim.Play(ranAttack == 0 ? "Attack" : "RangedAttack", 0, 0);

            ranAttack = Random.Range(0, 2);
            attackRange = ranAttack == 0 ? 2.5f : 10f;
        }

        else
        {
            anim.SetBool("Move", true);

            //몬스터 보는 방향
            direction.x = target.position.x < transform.position.x ? -1 : 1;
            transform.localScale = direction;

            //몬스터 이동 위치
            var movePos = target.position - transform.position;
            rigid.velocity = movePos.normalized * moveSpeed;
        }
    }
}
