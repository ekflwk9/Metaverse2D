using TMPro;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    private Animator anim;
    private TMP_Text damageText;

    public void SetComponent()
    {
        damageText = Service.FindChild(this.transform, "Text").GetComponent<TMP_Text>();
        anim = GetComponent<Animator>();
        this.gameObject.SetActive(false);
    }

    public void Show(Vector3 _spawnPos, int _damageValue)
    {
        damageText.text = _damageValue.ToString();
        this.transform.position = _spawnPos;

        if (this.gameObject.activeSelf) anim.Play("Idle", 0, 0);
        else this.gameObject.SetActive(true);
    }

    private void SetOff()
    {
        this.gameObject.SetActive(false);
    }
}
