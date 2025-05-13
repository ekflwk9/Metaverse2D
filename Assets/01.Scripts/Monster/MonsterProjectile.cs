using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] private GameObject destroyFxPrefab;

    private float speed;
    private int damage;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isHit = false;

    private System.Action<MonsterProjectile> returnToPoolCallback;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Initialize(Vector2 direction, float speed, int damage, System.Action<MonsterProjectile> returnToPool)
    {
        this.speed = speed;
        this.damage = damage;
        this.returnToPoolCallback = returnToPool;

        isHit = false;
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) return;
        isHit = true;

        if (collision.CompareTag("Player"))
        {
            GameManager.gameEvent.Hit(collision.gameObject.name, damage);
            SpawnDestroyFx();
            ReturnToPool();
        }
        //else if (collision.CompareTag("Level"))
        //{
        //    SpawnDestroyFx();
        //    ReturnToPool();
        //}
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
        rb.velocity = Vector2.zero;
        returnToPoolCallback?.Invoke(this);
        gameObject.SetActive(false);
    }
}
