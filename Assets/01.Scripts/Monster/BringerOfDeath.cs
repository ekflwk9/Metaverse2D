using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : Monster
{
    private void Attack()
    {
        if (Service.Distance(target.position, transform.position) < 2.3f)
        {
            GameManager.gameEvent.Hit("Player", dmg);
        }
    }
}
