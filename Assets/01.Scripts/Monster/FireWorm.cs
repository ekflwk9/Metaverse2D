using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorm : Monster
{
    private int count;
    private FireWormProjectile[] bullet;

    public override void SetMonster()
    {
        base.SetMonster();

        var projectile = Service.FindResource("Enemy", "Fireworm_Projectile");
        bullet = new FireWormProjectile[5];

        for (int i = 0; i < bullet.Length; i++)
        {
            var spawnProjectile = Instantiate(projectile);
            var projectileInfo = spawnProjectile.GetComponent<FireWormProjectile>();

            projectileInfo.SetBullet("Player", 4);
            bullet[i] = projectileInfo;
        }
    }

    private void Attack()
    {
        count++;
        if (count >= bullet.Length)
        {
            count = 0;
        }

        bullet[count].Fire(this.transform.position, GameManager.player.transform.position);
        bullet[count].SetRotate(this.transform.position, GameManager.player.transform.position);
    }
}