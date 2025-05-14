using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.player.OnHit(damage);
            Destroy(gameObject);
        }
    }
}
