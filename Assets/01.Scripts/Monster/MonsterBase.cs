using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{

    public enum MonsterType
    {
        BingerOfDeath,
        FireWorm,
        Necromancer,
        Shaman,
        Boss
    }

    [Header("Monster Type")]
    public MonsterType monsterType;

    [SerializeField] private SpriteRenderer characterRenderer;

    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    private float maxHealth;
    private float attackDamage;
    private float attackSpeed;


    void Start()
    {
        SetStats(monsterType);
    }


    void SetStats(MonsterType type)
    {
        switch(type)
        {
            case MonsterType.BingerOfDeath:
                moveSpeed = 1.4f;
                maxHealth = 4f;
                attackDamage = 1f;
                break;

            case MonsterType.FireWorm:
                moveSpeed = 1f;
                maxHealth = 4f;
                attackDamage = 1f;
                attackSpeed = 1f;
                break;

            case MonsterType.Necromancer:
                moveSpeed = 1f;
                maxHealth = 6f;
                attackDamage = 1f;
                attackSpeed = 1.5f;
                break;

            case MonsterType.Shaman:
                moveSpeed = 1.6f;
                maxHealth = 6f;
                attackDamage = 1f;
                attackSpeed = 2f;
                break;

            case MonsterType.Boss:
                moveSpeed = 2f;
                maxHealth = 12f;
                attackDamage = 1f;
                attackSpeed = 2.5f;
                break;

        }
    }
}
