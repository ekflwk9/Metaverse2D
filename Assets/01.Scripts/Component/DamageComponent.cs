using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    private Animator anim;
    private TMP_Text damageText;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        damageText = GetComponent<TMP_Text>();
    }

    public void Show(int _damageValue)
    {
        damageText.text = _damageValue.ToString();
    }

    private void SetOff()
    {
        this.gameObject.SetActive(false);
    }
}
