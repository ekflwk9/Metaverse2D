using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMoveBase : MonoBehaviour
{
    protected Transform player;
    protected MonsterBase monsterBase;
    protected float moveSpeed;

    protected virtual void Start()
    {
        player = GameManager.player.transform;
        monsterBase = GetComponent<MonsterBase>();
        moveSpeed = monsterBase.MoveSpeed;
    }

    public abstract void Move();
}
