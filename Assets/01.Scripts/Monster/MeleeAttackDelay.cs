using UnityEngine;

public class MeleeAnimationEventRelay : MonoBehaviour
{
    private MeleeAttack meleeAttack;

    void Start()
    {
        meleeAttack = GetComponentInParent<MeleeAttack>();
    }

    public void CallMeleeAttack()
    {
        if (meleeAttack != null)
        {
            meleeAttack.TriggerAttack();
        }
    }
}
