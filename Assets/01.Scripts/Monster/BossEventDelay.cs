using UnityEngine;

public class BossAnimationEventRelay : MonoBehaviour
{
    private BossAttack bossAttack;

    void Awake()
    {
        bossAttack = GetComponentInParent<BossAttack>();
    }

    // ��� �� ������ ���� (�ִϸ��̼� �߰� �̺�Ʈ)
    public void TriggerDashDamage()
    {
        bossAttack?.TriggerDashDamage();
    }

    // ���� ���� ó�� (�ִϸ��̼� �� �̺�Ʈ)
    public void EndAttack()
    {
        bossAttack?.EndAttack();
    }

    // ����ü �߻� Ÿ�̹� �̺�Ʈ (����)
    public void TriggerProjectile()
    {
        bossAttack?.TriggerProjectile();
    }
}
