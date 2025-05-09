using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    // 이동, 충돌, 파괴
    
    [SerializeField] private GameObject destroyFxPrefab;

    private float speed;
    private int damage;

    private Rigidbody2D _rigidbody;
    private Animator _anim;
    private bool isHit = false;

    private System.Action<MonsterProjectile> returnToPoolCallback;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    public void Initialize(Vector2 direction, float speed, int damage, System.Action<MonsterProjectile> returnToPool)
    {
        this.speed = speed;
        this.damage = damage;
        this.returnToPoolCallback = returnToPool;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = direction.normalized * speed;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) return;
        isHit = true;

        if (collision.CompareTag("Player"))
        {
            GameManager.gameEvent.Hit(collision.gameObject.name,damage);
            ReturnToPool();
        }
        else if (collision.CompareTag("Level"))
        {
            ReturnToPool();
        }

            SpawnDestroyFx();
    }

    private void SpawnDestroyFx()
    {
        if (destroyFxPrefab != null)
        {
            Instantiate(destroyFxPrefab, transform.position, Quaternion.identity);
        }
    }

    private void ReturnToPool()
    {
        _rigidbody.velocity = Vector2.zero;
        returnToPoolCallback?.Invoke(this);
        gameObject.SetActive(false);
    }
}
