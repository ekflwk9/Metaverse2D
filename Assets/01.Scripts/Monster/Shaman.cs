using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : Monster
{
    private int count;
    private Bullet[] bullet;

    public override void SetMonster()
    {
        base.SetMonster();

        var bulletResource = Service.FindResource("Enemy", "Shaman_Projectile");
        bullet = new Bullet[5];

        for (int i = 0; i < bullet.Length; i++)
        {
            var spawnBullet = Instantiate(bulletResource);
            var bulletInfo = spawnBullet.GetComponent<Bullet>();

            bulletInfo.SetBullet("Player", 10);
            bullet[i] = bulletInfo;
        }
    }

    private void Attack()
    {
        count++;
        if (count >= bullet.Length) count = 0;

        var thisPos = this.transform.position;
        var targetPos = GameManager.player.transform.position;

        bullet[count].Fire(thisPos, targetPos);
        bullet[count].SetRotate(thisPos, targetPos);
    }
} 
