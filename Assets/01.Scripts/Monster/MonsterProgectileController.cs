using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProgectileController : MonoBehaviour
{
    [SerializeField] private MonsterProjectile projectilePrefab;
    private int poolSize = 20;

    private Queue<MonsterProjectile> pool = new Queue<MonsterProjectile>();
    private List<MonsterProjectile> activeProjectiles = new List<MonsterProjectile>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            MonsterProjectile proj = Instantiate(projectilePrefab, transform);
            proj.gameObject.SetActive(false);
            pool.Enqueue(proj);
        }
    }

    public MonsterProjectile GetProjectile()
    {
        MonsterProjectile proj;
        if (pool.Count > 0)
        {
            proj = pool.Dequeue();
        }
        else
        {
            // 풀에 발사체 소진
            proj = Instantiate(projectilePrefab, transform);
            proj.gameObject.SetActive(false);
        }

        activeProjectiles.Add(proj);
        return proj;
    }

    public void ReturnProjectile(MonsterProjectile proj)
    {
        if (activeProjectiles.Contains(proj))
        {
            activeProjectiles.Remove(proj);
        }

        pool.Enqueue(proj);
    }

    public void Shoot(Vector2 position, Vector2 direction, float speed, int damage)
    {
        MonsterProjectile proj = GetProjectile();
        proj.transform.position = position;
        proj.gameObject.SetActive(true);

        proj.Initialize(direction, speed, damage, ReturnProjectile);
    }

    // 클리어 할 때 호출
    public void ClearAllProjectiles()
    {
        foreach (var proj in activeProjectiles)
        {
            proj.gameObject.SetActive(false);
            pool.Enqueue(proj);
        }
        activeProjectiles.Clear();
    }

}

