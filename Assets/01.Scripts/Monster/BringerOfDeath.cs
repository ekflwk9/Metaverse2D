using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : Monster
{
    private void Awake()
    {
        base.Awake();
    }
    private void Attack()
    {
        if (Service.Distance(target.position, transform.position) < 1.8f)
        {
            GameManager.gameEvent.Hit("Player", dmg);
        }
    }
}
