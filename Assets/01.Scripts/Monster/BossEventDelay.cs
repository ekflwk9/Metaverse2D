using UnityEngine;

public class BossAnimationEventRelay : MonoBehaviour
{
    private BossAttack bossAttack;

    void Awake()
    {
        bossAttack = GetComponentInParent<BossAttack>();
    }

    // 대시 중 데미지 적용 (애니메이션 중간 이벤트)
    public void TriggerDashDamage()
    {
        bossAttack?.TriggerDashDamage();
    }

    // 공격 종료 처리 (애니메이션 끝 이벤트)
    public void EndAttack()
    {
        bossAttack?.EndAttack();
    }

    // 투사체 발사 타이밍 이벤트 (선택)
    public void TriggerProjectile()
    {
        bossAttack?.TriggerProjectile();
    }
}
