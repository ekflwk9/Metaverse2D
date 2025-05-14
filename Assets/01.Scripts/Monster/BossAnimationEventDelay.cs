using UnityEngine;
using System.Collections;

public class BossAnimationEventDelay : MonoBehaviour
{
    private BossAttack bossAttack;

    void Start()
    {
        bossAttack = GetComponentInParent<BossAttack>();
    }

    public void TriggerDashDamage()
    {
        bossAttack?.TriggerDashDamage();
    }

    public void EndAttack()
    {
        bossAttack?.EndAttack();
    }

    public void TriggerProjectile()
    {
        StartCoroutine(FireMultipleProjectiles());
    }

    private IEnumerator FireMultipleProjectiles()
    {
        int count = 3;
        float delay = 0.5f;

        for (int i = 0; i < count; i++)
        {
            bossAttack?.TriggerProjectile();
            yield return new WaitForSeconds(delay);
        }
    }
}
