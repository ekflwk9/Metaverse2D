using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Monster
{
    private int count;
    private NecromancerBullet[] bullet;

    public override void Awake()
    {
        base.Awake();

        var bulletResource = Service.FindResource("Enemy", "Necromancer_Bullet");
        bullet = new NecromancerBullet[5];

        for (int i = 0; i < bullet.Length; i++)
        {
            var spawnBullet = Instantiate(bulletResource);
            var bulletInfo = spawnBullet.GetComponent<NecromancerBullet>();

            bulletInfo.SetBullet("Player", 10);
            bullet[i] = bulletInfo;
        }
    }

    private void Attack()
    {
        count++;
        if (count >= bullet.Length) count = 0;

        bullet[count].Fire(this.transform.position, GameManager.player.transform.position);
    }
}
