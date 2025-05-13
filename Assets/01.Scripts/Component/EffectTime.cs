using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTime : MonoBehaviour
{
    [Header("꺼지는 타이밍")]
    [SerializeField] private float offTime = 1f;

    private void Awake()
    {
        var anim = GetComponent<Animator>();
        anim.SetFloat("Speed", offTime);

        DontDestroyOnLoad(this.gameObject);
    }

    private void SetOff()
    {
        this.gameObject.SetActive(false);
    }
}
